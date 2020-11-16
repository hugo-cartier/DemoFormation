using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace WPFGestionStock.Behavior
{
    public class FormatReferenceBehavior : Behavior<TextBox>
    {
        private int _NbCharPremierePartie = 2;
        private int _NbCharDeuxiemePartie = 3;
        private string _oldValidValue = "";

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.TextChanged += AssociatedObject_TextChanged;
        }

        string reference = "XX-000";

        public bool IsCharValid(string s, string reference, int index)
        {
            if (reference[index] == 'X')
                return Char.IsLetter(s[index]);
            if (reference[index] == '-')
                return s[index] == '-';
            if (reference[index] == '0')
                return Char.IsDigit(s[index]);

            return false;
        }

        /// <summary>
        /// X
        /// XX
        /// XX-
        /// XX-1
        /// XX-12
        /// XX-123
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociatedObject_TextChanged(object sender, TextChangedEventArgs e)
        {
            var value = AssociatedObject.Text;
            var isValid = true;

            for (int i = 0; i < Math.Min(value.Length, reference.Length); i++)
            {
                isValid &= IsCharValid(value, reference, i);
            }

            if(!isValid)
            {
                int ss = AssociatedObject.SelectionStart;
                AssociatedObject.Text = _oldValidValue;
                AssociatedObject.SelectionStart = ss;
            }
            else
            {
                if (value.Length == 2)
                {
                    int ss = AssociatedObject.SelectionStart + 1;
                    AssociatedObject.Text = AssociatedObject.Text + "-";
                    AssociatedObject.SelectionStart = ss;
                }
                else if (value.Length > _NbCharPremierePartie + _NbCharDeuxiemePartie + 1)
                {
                    int ss = AssociatedObject.SelectionStart;
                    AssociatedObject.Text = _oldValidValue;
                    AssociatedObject.SelectionStart = ss;
                    return;
                }
                else
                {
                    _oldValidValue = AssociatedObject.Text;
                }
            }



            //if (value.Length > _NbCharPremierePartie + _NbCharDeuxiemePartie + 1)
            //{
            //    int ss = AssociatedObject.SelectionStart + 1;
            //    AssociatedObject.Text = _oldValidValue;
            //    AssociatedObject.SelectionStart = ss;
            //    return;
            //}

            //if (value.Length == 2)
            //{
            //    int ss = AssociatedObject.SelectionStart + 1;
            //    AssociatedObject.Text = _oldValidValue + "-";
            //    AssociatedObject.SelectionStart = ss;
            //    return;
            //}


            //for (int i = 0; i < value.Length; i++)
            //{
            //    if(i >= 0 && i < _NbCharPremierePartie)
            //    {
            //        if(!Char.IsLetter(value[i]))
            //        {
            //            int ss = AssociatedObject.SelectionStart;
            //            AssociatedObject.Text = _oldValidValue;
            //            AssociatedObject.SelectionStart = ss;
            //        }
            //        else
            //            _oldValidValue = AssociatedObject.Text;

            //    }
            //    else if(i == 2 && value.Length == 2)
            //    {
            //        if (value[i] != '-')
            //        {
            //            int ss = AssociatedObject.SelectionStart;
            //            AssociatedObject.Text = _oldValidValue;
            //            AssociatedObject.SelectionStart = ss;
            //        }
            //        else
            //            _oldValidValue = AssociatedObject.Text;
            //    }
            //    else if(i > _NbCharPremierePartie && i <= _NbCharPremierePartie + _NbCharDeuxiemePartie)
            //    {
            //        if (!Char.IsDigit(value[i]))
            //        {
            //            int ss = AssociatedObject.SelectionStart;
            //            AssociatedObject.Text = _oldValidValue;
            //            AssociatedObject.SelectionStart = ss;
            //        }
            //        else
            //            _oldValidValue = AssociatedObject.Text;
            //    }
            //    else
            //        _oldValidValue = AssociatedObject.Text;
            //}


            //int ValueLength = value.Length;
            //var TestChar = value.Substring(ValueLength - 1, 1);
            //if(ValueLength > 0 && ValueLength <= _NbCharPremierePartie)
            //{
            //    if(!TestChar.All(Char.IsLetter))
            //    {
            //        int ss = AssociatedObject.SelectionStart;
            //        AssociatedObject.Text = _oldValidValue;
            //        AssociatedObject.SelectionStart = ss;
            //    }
            //    else
            //        _oldValidValue = AssociatedObject.Text;
            //}
            //else if(ValueLength > _NbCharPremierePartie && ValueLength <= _NbCharDeuxiemePartie)
            //{
            //    if (!TestChar.All(Char.IsDigit))
            //    {
            //        int ss = AssociatedObject.SelectionStart;
            //        AssociatedObject.Text = _oldValidValue;
            //        AssociatedObject.SelectionStart = ss;
            //    }
            //    else
            //        _oldValidValue = AssociatedObject.Text;
            //}
            //else
            //{
            //    _oldValidValue = AssociatedObject.Text;
            //}





            //var PremierePartie = value.Substring(0, _NbCharPremierePartie);
            //var SecondePartie = value.Substring(_NbCharPremierePartie - 1, _NbCharDeuxiemePartie);
            //if(value.Length > 0 && value.Length <= _NbCharPremierePartie)
            //{
            //    if(!PremierePartie.All(Char.IsLetter))
            //    {
            //        int ss = AssociatedObject.SelectionStart;
            //        AssociatedObject.Text = _oldValidValue;
            //        AssociatedObject.SelectionStart = ss;
            //    }
            //    else
            //        _oldValidValue = AssociatedObject.Text;
            //}
            //else if (value.Length > _NbCharPremierePartie && value.Length <= _NbCharDeuxiemePartie)
            //{
            //    if (!SecondePartie.All(Char.IsDigit))
            //    {
            //        int ss = AssociatedObject.SelectionStart;
            //        AssociatedObject.Text = _oldValidValue;
            //        AssociatedObject.SelectionStart = ss;
            //    }
            //    else
            //        _oldValidValue = AssociatedObject.Text;
            //}
            //else
            //{
            //    _oldValidValue = AssociatedObject.Text;
            //}
        }

        protected override void OnDetaching()
        {
            AssociatedObject.TextChanged -= AssociatedObject_TextChanged;
            base.OnDetaching();
        }
    }
}
