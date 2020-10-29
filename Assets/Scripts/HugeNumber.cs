using System;
using UnityEngine;

public class HugeNumber
{
    public string numberValue = "0";

    public HugeNumber(string numberValue)
    {
        this.numberValue = numberValue;
    }

    public void AddNumber(HugeNumber number)
    {
        string finalNumberValue = "";
        int numberIterator = numberValue.Length - 1;

        // when sum of 2 chars is bigger than 10 then add 1 in next iteration
        bool addOneNext = false;

        for (int i = number.numberValue.Length - 1; i >= 0; i--)
        {
            int num = number.numberValue[i] - '0';
            // when current number is lower than other then insert rest from other on front
            if (numberIterator < 0)
            {
                if (addOneNext)
                {
                    num++;
                    addOneNext = false;
                }
                finalNumberValue = finalNumberValue.Insert(0, num.ToString());
                continue;
            }

            int current = numberValue[numberIterator] - '0';
            int final = num + current;

            // if previous sum was >10 then add 1 
            if (addOneNext)
            {
                final++;
                addOneNext = false;
            }

            string finalString = final.ToString();

            // if sum is >=10 then remove 1 to add to next iteration
            if (final >= 10)
            {
                addOneNext = true;
                finalString = finalString.Substring(1);
            }

            numberIterator--;
            finalNumberValue = finalNumberValue.Insert(0, finalString);

        }
        // if other number is lower than current then add what's left from current number
        if (numberIterator >= 0)
        {
            finalNumberValue = finalNumberValue.Insert(0, numberValue.Substring(0, numberIterator + 1));
        }
        else if (addOneNext)
        {
            finalNumberValue = finalNumberValue.Insert(0, "1");
        }
        numberValue = finalNumberValue;
    }

    public bool CanSubtract(HugeNumber number)
    {
        int numberLength = number.numberValue.Length;

        if (numberValue.Length < numberLength)
        {
            return false;
        }
        if (numberValue.Length > numberLength)
        {
            return true;
        }

        for (int i = 0; i < numberValue.Length; i++)
        {
            if (numberValue[i] < number.numberValue[i])
            {
                return false;
            }
        }
        return true;
    }

    public void SubtractNumber(HugeNumber number)
    {
        string finalNumberValue = "";

        int lengthDifference = numberValue.Length - number.numberValue.Length;

        // Initially take carry zero 
        int carry = 0;

        // Traverse from end of both strings 
        for (int i = number.numberValue.Length - 1; i >= 0; i--)
        {
            // Do school mathematics, compute  
            // difference of current digits and carry 
            int subtraction = ((numberValue[i + lengthDifference] - '0') - (number.numberValue[i] - '0') - carry);
            if (subtraction < 0)
            {
                subtraction = subtraction + 10;
                carry = 1;
            }
            else
                carry = 0;

            finalNumberValue += subtraction.ToString();
        }

        // subtract remaining digits of numberValue[] 
        for (int i = lengthDifference - 1; i >= 0; i--)
        {
            if (numberValue[i] == '0' && carry > 0)
            {
                finalNumberValue += "9";
                continue;
            }
            int subtraction = ((numberValue[i] - '0') - carry);

            // remove preceding 0's 
            if (i > 0 || subtraction > 0)
            {
                finalNumberValue += subtraction.ToString();
            }
            carry = 0;

        }

        // reverse resultant string 
        char[] reveerseResultArray = finalNumberValue.ToCharArray();
        Array.Reverse(reveerseResultArray);
        numberValue = new string(reveerseResultArray);
    }

    public void MultiplyByNumber(HugeNumber number)
    {
        string finalNumberValue = "";

        if (numberValue.Length == 0 || number.numberValue.Length == 0 ||
            numberValue == "0" || number.numberValue == "0")
        {
            numberValue = "0";
            return;
        }

        // will keep the result number in vector  
        // in reverse order  
        int[] resultReversed = new int[numberValue.Length + number.numberValue.Length];

        // Below two indexes are used to  
        // find positions in result.  
        int indexN1 = 0;
        int i;

        // Go from right to left in num1  
        for (i = numberValue.Length - 1; i >= 0; i--)
        {
            int carry = 0;
            int n1 = numberValue[i] - '0';

            // To shift position to left after every  
            // multipliccharAtion of a digit in num2  
            int indexN2 = 0;

            // Go from right to left in num2              
            for (int j = number.numberValue.Length - 1; j >= 0; j--)
            {
                // Take current digit of second number  
                int n2 = number.numberValue[j] - '0';

                // Multiply with current digit of first number  
                // and add result to previously stored result  
                // charAt current position.  
                int sum = n1 * n2 + resultReversed[indexN1 + indexN2] + carry;

                // Carry for next itercharAtion  
                carry = sum / 10;

                // Store result  
                resultReversed[indexN1 + indexN2] = sum % 10;

                indexN2++;
            }

            // store carry in next cell  
            if (carry > 0)
            {
                resultReversed[indexN1 + indexN2] += carry;
            }

            // To shift position to left after every  
            // multipliccharAtion of a digit in num1.  
            indexN1++;
        }

        // ignore '0's from the right  
        i = resultReversed.Length - 1;
        while (i >= 0 && resultReversed[i] == 0)
        {
            i--;
        }

        // If all were '0's - means either both  
        // or one of num1 or num2 were '0'  
        if (i == -1)
        {
            numberValue = "0";
            return;
        }

        while (i >= 0)
        {
            finalNumberValue += (resultReversed[i--]);
        }

        numberValue = finalNumberValue;
    }

    public string GetFormattedNumberString(HugeNumberDisplayFormat format)
    {
        string formattedNumberValue = "";
        switch (format)
        {
            case HugeNumberDisplayFormat.NONE:
                formattedNumberValue = numberValue;
                break;
            case HugeNumberDisplayFormat.SPACED:
                // return formatted number with spaces every 3 digits
                int spaceIndicator = 3;
                for (int i = numberValue.Length - 1; i >= 0; i--)
                {
                    formattedNumberValue = formattedNumberValue.Insert(0, numberValue[i].ToString());
                    spaceIndicator--;
                    if (spaceIndicator == 0)
                    {
                        formattedNumberValue = formattedNumberValue.Insert(0, " ");
                        spaceIndicator = 3;
                    }
                }
                break;
            case HugeNumberDisplayFormat.INDEXED:
                // return formatted number with index: A,B,C ....
                int indexIndicator = 4;

                int indexNumber = Mathf.CeilToInt(numberValue.Length / indexIndicator);
                int restFromDividing = numberValue.Length % indexIndicator;

                HugeNumberIndex currentIndex = (HugeNumberIndex)indexNumber;
                if (restFromDividing == 0)
                {
                    currentIndex--;
                }
                formattedNumberValue = formattedNumberValue.Insert(0, currentIndex.ToString());
                formattedNumberValue = formattedNumberValue.Insert(0, numberValue.Substring(0, Mathf.Min(indexIndicator, numberValue.Length)));
                if (restFromDividing != 0)
                {
                    formattedNumberValue = formattedNumberValue.Insert(restFromDividing, ",");
                }

                break;
        }

        return formattedNumberValue;
    }
}