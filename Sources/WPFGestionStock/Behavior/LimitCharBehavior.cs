using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace WPFGestionStock.Behavior
{
    public class LimitCharBehavior : Behavior<TextBox>
    {
        private int _NbCharMax = 30;
        private string _oldValidValue = "";

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.TextChanged += AssociatedObject_TextChanged;
        }

        private void AssociatedObject_TextChanged(object sender, TextChangedEventArgs e)
        {
            var value = AssociatedObject.Text;
            if(value.Length > _NbCharMax)
            {
                int ss = AssociatedObject.SelectionStart;
                AssociatedObject.Text = _oldValidValue;
                AssociatedObject.SelectionStart = ss;
            }
            else
            {
                _oldValidValue = AssociatedObject.Text;
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.TextChanged -= AssociatedObject_TextChanged;
            base.OnDetaching();
        }
    }
}
