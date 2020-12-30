using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Day25
    {

        static Int64 transform(Int64 subject, Int64 loop)
        {
            Int64 value = 1;
            for (int _ = 0; _ < loop; _++)
            {
                value *= subject;
                value %= 20201227;
            }
            return value;
        }
        public static Int64 Part1()
        {
            //Int64 rfidPublicKey = 5764801;  Test Value
            //Int64 doorPublicKey = 17807724; Test Value


            int rfidPublicKey = 19241437;
            int doorPublicKey = 17346587;
            Int64 divisionValue = 20201227;

            Int64 value = 1;
            Int64 subjectValue = 7;
            Int64 loop = 0;
            while(true)
            {
                value *= subjectValue;
                value %= divisionValue;
                loop++;
                if(value == rfidPublicKey)
                {
                    Console.WriteLine($"Rfid loop = {loop}");
                    return transform(doorPublicKey, loop);
                }
                if (value == doorPublicKey)
                {
                    Console.WriteLine($"Door loop = {loop}");
                    return transform(rfidPublicKey, loop);
                }
            }
        }
    }
}
