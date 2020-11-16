using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace WPFGestionStock.Behavior
{
    public class ForceNumericBehavior : Behavior<TextBox>
    {
        private string _oldValidValue = "0";
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.TextChanged += AssociatedObject_TextChanged;
        }

        private void AssociatedObject_TextChanged(object sender, TextChangedEventArgs e)
        {
            var value = AssociatedObject.Text;
            float valueFloat;
            if(!float.TryParse(value, out valueFloat))
            {
                int ss = AssociatedObject.SelectionStart;
                AssociatedObject.Text = _oldValidValue;
                AssociatedObject.SelectionStart = ss;
            }
            else
            { 
                if(CanBeNegative == false && valueFloat < 0)
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
        }

        protected override void OnDetaching()
        {
            AssociatedObject.TextChanged -= AssociatedObject_TextChanged;
            base.OnDetaching();
        }

        #region CanBeNegative

        /// <summary>
        /// CanBeNegative Dependency Property
        /// </summary>
        public static readonly DependencyProperty CanBeNegativeProperty =
            DependencyProperty.Register("CanBeNegative", typeof(bool), typeof(ForceNumericBehavior),
                new FrameworkPropertyMetadata((bool)true));

        /// <summary>
        /// Gets or sets the CanBeNegative property. This dependency property 
        /// indicates ....
        /// </summary>
        public bool CanBeNegative
        {
            get { return (bool)GetValue(CanBeNegativeProperty); }
            set { SetValue(CanBeNegativeProperty, value); }
        }

        #endregion


    }
}
