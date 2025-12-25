using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SalesManagement_SysDev.Common
{
    internal class DataInputFormCheck
    {
        ///////////////////////////////
        //メソッド名：CheckFullWidth()
        //引　数   ：文字列
        //戻り値   ：True or False
        //機　能   ：全角文字列チェック
        //          ：全角文字列の場合True
        //          ：全角文字列でない場合False
        ///////////////////////////////
        public bool CheckFullWidth(string text)
        {
            bool flg;

            //[text.Replace("\r\n", string.Empty).Length]:text内の改行を排除(改行を無い状態に置き換える状態に)し文字数を数える
            int textLength = text.Replace("\r\n", string.Empty).Length;
            //[Encoding~]:改行を排除したtextのバイト数をShift_JISで変換して取得する
            int textByte = Encoding.GetEncoding("Shift_JIS").GetByteCount(text.Replace("\r\n", string.Empty));
            //全角文字は基本２バイト分。等しくない＝全角文字のみではない
            if (textByte != textLength *2)
            {
                flg = false;
            }
            else
            {
                flg = true;
            }

            return flg;
        }

        ///////////////////////////////
        //メソッド名：CheckHalfAlphabetNumeric()
        //引　数   ：文字列
        //戻り値   ：True or False
        //機　能   ：半角英数字チェック
        //          ：半角英数字文字列の場合True
        //          ：半角英数字文字列でない場合False
        ///////////////////////////////
        public bool CheckHalfAlphabetNumeric(string text)
        {
            bool flg;

            //[Regex]:正規表現を設定する
            Regex regex = new Regex("^[a-zA-Z0-9]+$");
            if(!regex.IsMatch(text))
            {
                return false;
            }
            else
            {
                flg = true;
            }

            return flg;
        }

        ///////////////////////////////
        //メソッド名：CheckNumeric()
        //引　数   ：文字列
        //戻り値   ：True or False
        //機　能   ：数値チェック
        //          ：数値の場合True
        //          ：数値でない場合False
        ///////////////////////////////
        public bool CheckNumeric(string text)
        {
            bool flg;

            //[Regex]:正規表現を設定する
            Regex regex = new Regex("^[0-9]+$");
            if (!regex.IsMatch(text))
            {
                return false;
            }
            else
            {
                flg = true;
            }

            return flg;
        }

        ///////////////////////////////
        //メソッド名：CheckPostalDigit()
        //引　数   ：文字列
        //戻り値   ：True or False
        //機　能   ：郵便番号桁数チェック
        //          ：郵便番号桁数が7桁の場合True
        //          ：郵便番号が7桁でない場合False
        ///////////////////////////////
        public bool CheckPostalDigit(string text)
        {
            bool flg;

            int textLength = text.Replace("\r\n", string.Empty).Length;
            //[Regex]:正規表現を設定する
            //7桁
            Regex regex = new Regex("^[0-9]+$");
            if (!regex.IsMatch(text) || textLength != 7)
            {
                return false;
            }
            else
            {
                flg = true;
            }

            return flg;
        }

        ///////////////////////////////
        //メソッド名：CheckPhoneDigitHyphen()
        //引　数   ：文字列
        //戻り値   ：True or False
        //機　能   ：電話番号桁数ハイフンチェック
        //          ：電話番号桁数ハイフンの場合True
        //          ：電話番号桁数ハイフンでない場合False
        ///////////////////////////////
        public bool CheckPhoneDigitHyphen(string text)
        {
            bool flg;

            int textLength = text.Replace("\r\n", string.Empty).Length;
            //[Regex]:正規表現を設定する
            //9桁以上11桁以下 + ハイフン２つ = 11以上13以下
            // = 11未満13より大きい
            Regex regex = new Regex("^[0-9]+-[0-9]+-[0-9]+$");
            if (!regex.IsMatch(text) || (textLength < 11 || textLength > 13))
            {
                return false;
            }
            else
            {
                flg = true;
            }

            return flg;
        }

        ///////////////////////////////
        //メソッド名：CheckFaxDigitHyphen()
        //引　数   ：文字列
        //戻り値   ：True or False
        //機　能   ：ファックス桁数ハイフンチェック
        //          ：ファックス桁数ハイフンの場合True
        //          ：ファックス桁数ハイフンでない場合False
        ///////////////////////////////
        public bool CheckFaxDigitHyphen(string text)
        {
            bool flg;

            int textLength = text.Replace("\r\n", string.Empty).Length;
            //[Regex]:正規表現を設定する
            //9桁以上11桁以下
            Regex regex = new Regex("^[0-9]+-[0-9]+-[0-9]+$");
            if (!regex.IsMatch(text) || (textLength < 11 || textLength > 13))
            {
                return false;
            }
            else
            {
                flg = true;
            }

            return flg;
        }
    }
}
