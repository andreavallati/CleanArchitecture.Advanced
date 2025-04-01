using FluentValidation;
using CleanArchitecture.Advanced.Client.Application.Interfaces.Services.Factory;

namespace CleanArchitecture.Advanced.Client.Presentation.ViewModels.Base
{
    public abstract class ViewModelBase<TValidationModel> : BindableBase
        where TValidationModel : class
    {
        protected readonly IFactoryUIService _uiService;
        protected readonly IValidator<TValidationModel> _validator;

        private bool _validationCalled;

        protected ViewModelBase(IFactoryUIService uiService, IValidator<TValidationModel> validator)
        {
            _uiService = uiService ?? throw new ArgumentNullException(nameof(uiService));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        protected virtual async Task<bool> ValidateAsync(TValidationModel validationModel)
        {
            //ClearAllErrors();

            if (_validator is null)
            {
                return true;
            }

            var result = await _validator.ValidateAsync(validationModel);

            _validationCalled = true;
            if (!result.IsValid)
            {
                result.Errors.ForEach(e =>
                {
                    var propertyName = e.PropertyName;

                    //AddError(propertyName, e.ErrorMessage);
                    //RaisePropertyChanged(propertyName);
                });
            }
            return result.IsValid;
        }
    }
}
