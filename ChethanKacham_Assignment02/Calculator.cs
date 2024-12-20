using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChethanKacham_Assignment02
{
    internal class Calculator
    {
        public string PerformAddition(string number1, string number2)
        {
            try
            {
                char[] bigNumber = number1.Reverse().ToArray();
                char[] smallNumber = number2.Reverse().ToArray();
                if (smallNumber.Length > bigNumber.Length)
                {
                    bigNumber = number2.Reverse().ToArray();
                    smallNumber = number1.Reverse().ToArray();
                }

                StringBuilder sum = new StringBuilder();
                int carry = 0;
                int i;
                for (i = 0; i < smallNumber.Length; i++)
                {
                    int digitSum = smallNumber[i] - '0' + bigNumber[i] - '0' + carry;
                    carry = 0;
                    if (digitSum > 9)
                    {
                        carry = 1;
                        digitSum -= 10;
                    }
                    sum.Append(digitSum);
                }

                while (i < bigNumber.Length)
                {
                    int digitSum = bigNumber[i] - '0' + carry;
                    carry = 0;
                    if (digitSum > 9)
                    {
                        carry = 1;
                        digitSum -= 10;
                    }
                    sum.Append(digitSum);
                    i++;
                }
                if (carry > 0)
                    sum.Append(carry);
                return new string(sum.ToString().Reverse().ToArray());
            }
            catch (Exception ex)
            {
                return "Exception Occured: " + ex.Message;
            }
        }

        public string PerformSubstraction(string number1, string number2)
        {
            try
            {
                char[] bigNumber = number1.Reverse().ToArray();
                char[] smallNumber = number2.Reverse().ToArray();
                bool isSmaller = IsSmaller(number1, number2);
                if (isSmaller)
                {
                    bigNumber = number2.Reverse().ToArray();
                    smallNumber = number1.Reverse().ToArray();
                }

                StringBuilder subResult = new StringBuilder();
                int i;
                int carry = 0;
                for (i = 0; i < smallNumber.Length; i++)
                {
                    int a = bigNumber[i] - '0';
                    int b = smallNumber[i] - '0';
                    int diff = a - b - carry;
                    if (diff < 0)
                    {
                        diff += 10;
                        carry = 1;
                    }
                    else
                    {
                        carry = 0;
                    }
                    subResult.Append(diff);
                }

                while (i < bigNumber.Length)
                {
                    int diff = (bigNumber[i] - '0') - carry;
                    if (diff < 0)
                    {
                        diff += 10;
                        carry = 1;
                    }
                    else
                    {
                        carry = 0;
                    }
                    subResult.Append(diff);
                    i++;
                }

                int j = subResult.Length - 1;

                while (subResult[j] == '0' && j > 0)
                    subResult.Remove(j--, 1);

                if (isSmaller)
                    subResult.Append('-');

                return new string(subResult.ToString().Reverse().ToArray());
            }
            catch (Exception ex)
            {
                return "Exception Occured: " + ex.Message;
            }
        }

        public string PerformMultiplication(string number1, string number2)
        {
            try
            {
                int num1Len = number1.Length;
                int num2Len = number2.Length;
                if (num1Len == 0 || num2Len == 0 || number1 == "0" || number2 == "0")
                {
                    return "0";
                }

                int[] result = new int[num1Len + num2Len];

                int num1Shift = 0;
                int i;

                for (i = num1Len - 1; i >= 0; i--)
                {
                    int carry = 0;
                    int n1 = number1[i] - '0';

                    int num2Shift = 0;

                    for (int j = num2Len - 1; j >= 0; j--)
                    {
                        int n2 = number2[j] - '0';

                        // Multiply with current digit of first number
                        // and add result to previously stored result
                        // at current position.
                        int sum = n1 * n2 + result[num1Shift + num2Shift] + carry;

                        carry = sum / 10;

                        result[num1Shift + num2Shift] = sum % 10;

                        num2Shift++;
                    }

                    if (carry > 0)
                        result[num1Shift + num2Shift] += carry;

                    num1Shift++;
                }

                // ignore '0's from the right
                i = result.Length - 1;
                while (i >= 0 && result[i] == 0)
                    i--;

                // If all were '0's - means either both
                // or one of num1 or num2 were '0'
                if (i == -1)
                {
                    return "0";
                }

                string finalResult = "";

                while (i >= 0)
                    finalResult += (result[i--]);

                return finalResult;
            }
            catch (Exception ex)
            {
                return "Exception Occured: " + ex.Message;
            }
        }

        public string PerformDivision(string number1, string number2)
        {
            try
            {
                string result = "";

                // Find prefix of number that is larger
                // than divisor(number2).
                int index = 0;
                ulong num2 = ulong.Parse(number2);
                ulong temp = (ulong)(number1[index] - '0');
                while (temp < num2)
                {
                    temp = temp * 10 + (ulong)(number1[index + 1] - '0');
                    index++;
                }
                ++index;

                // Repeatedly divide divisor with temp value. After
                // every division, update temp value to include one
                // more digit.
                while (number1.Length > index)
                {
                    result += (char)(temp / num2 + '0');

                    // Take next digit of entered number1
                    temp = (temp % num2) * 10 + (ulong)(number1[index] - '0');
                    index++;
                }
                result += (char)(temp / num2 + '0');

                // If divisor(number2) is greater than number1 display result as zero
                if (result.Length == 0)
                {
                    return "0";
                }

                return result;
            }
            catch (DivideByZeroException ex)
            {
                return ex.Message + ", please enter valid number";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static bool IsSmaller(string number1, string number2)
        {
            int n1 = number1.Length, n2 = number2.Length;
            if (n1 < n2)
                return true;
            if (n2 < n1)
                return false;

            for (int i = 0; i < n1; i++)
                if (number1[i] < number2[i])
                    return true;
                else if (number1[i] > number2[i])
                    return false;

            return false;
        }
    }
}
