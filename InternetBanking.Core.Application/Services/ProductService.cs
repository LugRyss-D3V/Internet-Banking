using AutoMapper;
using InternetBanking.Core.Application.Dtos.Account.Response;
using InternetBanking.Core.Application.Dtos.Product.Request;
using InternetBanking.Core.Application.Dtos.Product.Response;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Products;
using InternetBanking.Core.Domain.Common;
using InternetBanking.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace InternetBanking.Core.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse? _user;

        public ProductService(IProductRepository repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _user = httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task Add(ProductSaveViewModel vm)
        {
            if (vm is SavingAccountViewModel savingAccountVM)
            {
                SavingAccount entity = _mapper.Map<SavingAccount>(savingAccountVM);
                entity.CreatedBy = _user.Id;
                await _repository.AddAsync(entity);
            }
            else if (vm is LoanViewModel loanVM)
            {
                Loan entity = _mapper.Map<Loan>(loanVM);
                entity.CreatedBy = _user.Id;
                await _repository.AddAsync(entity);
            }
            else if (vm is CreditCardViewModel creditCardVM)
            {
                CreditCard entity = _mapper.Map<CreditCard>(creditCardVM);
                entity.CreatedBy = _user.Id;
                await _repository.AddAsync(entity);
            }
        }
        public async Task<DepositResponse> Deposit(DepositRequest request)
        {
            DepositResponse response = new DepositResponse();

            try
            {
                if (request.ProductType == ProductType.SavingAccount)
                {
                    SavingAccount entity = (SavingAccount)await _repository.GetByIdAsync(request.ProductId);

                    entity.Cuantity += request.Amount;
                    await _repository.UpdateAsync(entity, request.ProductId);
                }
                else if (request.ProductType == ProductType.Loan)
                {
                    Loan entity = (Loan)await _repository.GetByIdAsync(request.ProductId);
                    entity.Cuantity -= request.Amount;

                    if (entity.Cuantity < 0)
                    {
                        double remainingAmount = Math.Abs(entity.Cuantity);
                        entity.Cuantity = 0;

                        DepositResponse deposit = await DepositOnMainAccount(entity.UserId, remainingAmount);

                        if (deposit.HasError)
                        {
                            response.HasError = true;
                            response.Error = "Ha habido un error al pasar el restante a tu cuenta principal, el pago del prestamo quedo cancelado";
                        }
                    }

                    await _repository.UpdateAsync(entity, request.ProductId);
                }
                else if (request.ProductType == ProductType.CreditCard)
                {
                    CreditCard entity = (CreditCard)await _repository.GetByIdAsync(request.ProductId);

                    if(entity.Cuantity < 0)
                    {
                        double remainingAmount = Math.Abs(entity.Cuantity);
                        entity.Cuantity = 0;

                        DepositResponse deposit = await DepositOnMainAccount(entity.UserId, remainingAmount);

                        if (deposit.HasError)
                        {
                            response.HasError = true;
                            response.Error = "Ha habido un error al pasar el restante a tu cuenta principal, el pago de la tarjeta quedo cancelado";
                        }
                    }

                    await _repository.UpdateAsync(entity, request.ProductId);
                }
                else
                {
                    response.HasError = true;
                    response.Error = "Tipo de producto invalido";
                }
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Error = ex.ToString();
                return response;
            }

            return response;
        }
        public async Task<DepositResponse> DepositOnMainAccount(string userId, double Amount)
        {
            DepositResponse response = new DepositResponse();
            try
            {
                SavingAccount entity = await _repository.GetMainAccount(userId);
                if (entity != null)
                {
                    entity.Cuantity += Amount;
                    await _repository.UpdateAsync(entity, entity.ProductId);
                    response.ProductId = entity.ProductId;
                }
                else
                {
                    response.HasError = true;
                    response.Error = "No tiene cuenta de ahorros principal";
                    return response;
                }
            }
            catch(Exception ex)
            {
                response.HasError = true;
                response.Error = ex.ToString();
                return response;
            }
            
            return response;
        }
        public async Task<DepositResponse> DepositOnMainAccount(SavingAccount entity, double Amount)
        {
            DepositResponse response = new DepositResponse();
            try
            {
                if (entity != null)
                {
                    entity.Cuantity += Amount;
                    await _repository.UpdateAsync(entity, entity.ProductId);
                    response.ProductId = entity.ProductId;
                }
                else
                {
                    response.HasError = true;
                    response.Error = "No tiene cuenta de ahorros principal";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Error = ex.ToString();
                return response;
            }

            return response;
        }

        public async Task<DebitResponse> Debit(DebitRequest request)
        {
            DebitResponse response = new DebitResponse();

            try
            {
                if (request.ProductType == ProductType.SavingAccount)
                {
                    SavingAccount entity = (SavingAccount) await _repository.GetByIdAsync(request.ProductId);

                    if (entity.Cuantity >= request.Amount)
                    {
                        entity.Cuantity -= request.Amount;
                        await _repository.UpdateAsync(entity, request.ProductId);
                    }
                    else
                    {
                        response.HasError = true;
                        response.Error = "No cuenta con saldo suficiente";
                    }

                    return response;
                }
                else if (request.ProductType == ProductType.Loan)
                {
                    Loan entity = (Loan)await _repository.GetByIdAsync(request.ProductId);
                    entity.Cuantity += request.Amount;

                    await _repository.UpdateAsync(entity, request.ProductId);
                }
                else if (request.ProductType == ProductType.CreditCard)
                {
                    CreditCard entity = (CreditCard)await _repository.GetByIdAsync(request.ProductId);

                    if (entity.Cuantity + request.Amount <= entity.Limit)
                    {
                        entity.Cuantity += request.Amount;
                        await _repository.UpdateAsync(entity, request.ProductId);
                    }
                    else
                    {
                        response.HasError = true;
                        response.Error = "La cantidad sobrepasa el limite de su tarjeta";
                    }
                }
                else
                {
                    response.HasError = true;
                    response.Error = "Tipo de producto invalido";
                }
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.Error = ex.ToString();
                return response;
            }

            return response;
        }
        public async Task Delete(string id)
        {
            var entity = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(entity);
        }
        
        public async Task<int> GetProductsCount()
        {
            return await _repository.GetProductsTotal();
        }
        public async Task<List<ProductViewModel>> GetAll()
        {
            var entities = await _repository.GetAllAsync();
            List<ProductViewModel> result = _mapper.Map<List<ProductViewModel>>(entities);

            return result;
        }
        public async Task<List<ProductViewModel>> GetAllById(string id)
        {
            var entities = await _repository.GetAllById(id);
            List<ProductViewModel> result = _mapper.Map<List<ProductViewModel>>(entities);

            return result;
        }

        public async Task<ProductViewModel> GetById(string id)
        {
            var entities = await _repository.GetByIdAsync(id);
            ProductViewModel result = _mapper.Map<ProductViewModel>(entities);

            return result;
        }

        public async Task<List<TViewModel>> GetAllByUserAndMap<T, TViewModel>(string userId)
            where T : Products
            where TViewModel : ProductSaveViewModel
        {
            var entities = await _repository.GetUserEntities<T>(userId);

            List<TViewModel> result = _mapper.Map<List<TViewModel>>(entities);

            return result;
        }
        
        public async Task<SavingAccount> GetMainAccount(string userId)
        {
            return await _repository.GetMainAccount(userId);
        } 

    }
}
