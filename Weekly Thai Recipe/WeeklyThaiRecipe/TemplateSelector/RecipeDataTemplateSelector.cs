namespace WeeklyThaiRecipe.TemplateSelector
{
    using System.Windows;

    using WeeklyThaiRecipe.ViewModels;

    public class RecipeDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate FirstTemplate
        {
            get;
            set;
        }

        public DataTemplate NormalTemplate
        {
            get;
            set;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var recipeItem = item as RecipeViewModel;
            if (recipeItem != null)
            {
                if (recipeItem.IsFirst)
                {
                    return FirstTemplate;
                }

                return NormalTemplate;
            } 

            return base.SelectTemplate(item, container);
        }
    }
}
