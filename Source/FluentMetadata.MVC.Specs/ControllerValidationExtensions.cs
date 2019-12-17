using System;
using System.Web.Mvc;

namespace FluentMetadata.MVC.Specs
{
    public static class ControllerValidationExtensions
    {
        public static void ValidateModel(this Controller controller, object model, string prefix = null)
        {
            if (controller == null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            EnsureControllerContext(controller);

            foreach (var validationResult in model.GetModelValidator(controller).Validate(model))
            {
                controller.AddModelError(prefix, validationResult);
            }
        }

        private static void EnsureControllerContext(ControllerBase controller)
        {
            if (controller.ControllerContext == null)
            {
                controller.ControllerContext = new ControllerContext();
            }
        }

        private static ModelValidator GetModelValidator(this object model, ControllerBase controller)
        {
            return ModelValidator.GetModelValidator(
                ModelMetadataProviders.Current.GetMetadataForType(() => model, model.GetType()),
                controller.ControllerContext);
        }

        private static void AddModelError(this Controller controller, string prefix, ModelValidationResult validator)
        {
            controller.ModelState.AddModelError(CreateSubPropertyName(prefix, validator.MemberName), validator.Message);
        }

        private static string CreateSubPropertyName(string prefix, string propertyName)
        {
            if (string.IsNullOrEmpty(prefix))
            {
                return propertyName;
            }

            if (string.IsNullOrEmpty(propertyName))
            {
                return prefix;
            }

            return prefix + "." + propertyName;
        }
    }
}