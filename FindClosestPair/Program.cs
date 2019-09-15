using System;
using System.Linq;

namespace FindClosestPair
{
    static class Program
    {
        static void Main(string[] args)
        {
            var A = new int[] { 2, 6, 1, 0, 5, 7, 4, 3 };
            var B = new int[] { 3, 4, 7, 5, 2, 1, 0, 6 };
            var resultString = FindClosest(A, B, 5);
        }

        public static string FindClosest(int[] dataA, int[] dataB, int target)
        {
            int numberOfLoop = 0, totalClosest = 0, minDiff = int.MaxValue;
            dataA = dataA.OrderBy(a => Math.Abs(a - ((decimal)target / 2))).ToArray();  //--->>> O(NlogN) sort ข้อมูลที่มีระยะห่าง จาก target น้อยที่สุดไว้แรกๆ จะได้ Hit บ่อย
            dataB = dataB.OrderBy(b => Math.Abs(b - ((decimal)target / 2))).ToArray();  //--->>> O(NlogN) sort ข้อมูลที่มีระยะห่าง จาก target น้อยที่สุดไว้แรกๆ จะได้ Hit บ่อย                

            var dataA2 = dataA.OrderBy(a => Math.Abs(a - ((decimal)target / 2))).ThenBy(a => a).ToArray();  //--->>> O(NlogN) sort ข้อมูลที่มีระยะห่าง จาก target น้อยที่สุดไว้แรกๆ จะได้ Hit บ่อย
            var dataB2 = dataB.OrderBy(b => Math.Abs(b - ((decimal)target / 2))).ThenBy(b => b).ToArray();  //--->>> O(NlogN) sort ข้อมูลที่มีระยะห่าง จาก target น้อยที่สุดไว้แรกๆ จะได้ Hit บ่อย           

            Console.WriteLine("A is : " + string.Join(",", dataA));
            Console.WriteLine("B is : " + string.Join(",", dataB));
            Console.WriteLine("A2 is : " + string.Join(",", dataA2));
            Console.WriteLine("A2 is : " + string.Join(",", dataB2));


            var consoleResult = string.Empty;
            int lengthA = dataA.Length, lengthB = dataB.Length;
            int lengthLoop = (lengthA < lengthB) ? lengthA : lengthB;                   //เลือก Loop ที่น้อยที่สุดจาก A หรือ B เพื่อใช้วน

            for (int i = 0; i < lengthLoop; i++)                                        //---->O(N/(target+1)) Loop เท่ากับจำนวน A , B หรือ target + 1 ที่น้อยที่สุด (ถ้าใส่ target เป็น 1 จะวนแค่ 2 ครั้ง (0,1)(1,0))
            {
                numberOfLoop++;                                                         //เช็ค performance ตอนจบโปรแกรม loop ไปทั้งหมดกี่ครั้ง
                int diff = Math.Abs(dataA[i] + dataB[i] - target);                      //หา 
                if (diff != 0 && diff <= target * 2 && i + 1 < lengthLoop)              //เช็คว่าเป็น diff ทีสามารถ swap แล้ว diff กลายเป็น 0 ได้
                {
                    if (Math.Abs(dataA[i + 1] + dataB[i] - target) == 0)                //เช็ค swap A แล้วทำให้ diff เป็น 0
                    {
                        int tempA = dataA[i];
                        dataA[i] = dataA[i + 1];
                        dataA[i + 1] = tempA;
                    }
                    else if (Math.Abs(dataA[i] + dataB[i + 1] - target) == 0)           //เช็ค swap B แล้วทำให้ diff เป็น 0
                    {
                        int tempB = dataB[i];
                        dataB[i] = dataB[i + 1];
                        dataB[i + 1] = tempB;
                    }
                    diff = Math.Abs(dataA[i] + dataB[i] - target);                      //คำนวน diff ใหม่หลัง swap (diff กลายเป็น 0)
                }

                if (diff == minDiff)                                                    //จับคู่แล้ว เท่ากับ target (diff 0)
                {
                    totalClosest += 1;
                    consoleResult += "\n(" + dataA[i] + " , " + dataB[i] + ")";         //Add console ไว้โชวคู่ผลลัพท์ที่รวมกันแล้วใกล้ tar get มากที่สุด
                    if (diff == 0)
                    {
                        continue;                                                       //continue เลยไม่ต้องเช็ตข้างล่างต่อ เพราะคู่นี้รวมกันได้เท่ากับ target แล้ว
                    }
                }
                else if (diff < minDiff)                                                //เข้าเฉพาะครั้งแรก เพราะ sort ของมาหมดแล้ว ไม่มีทางที่ครั้งต่อไปจะ diff ได้น้อยกว่านี้
                {
                    totalClosest = 1;
                    consoleResult = "\n(" + dataA[i] + " , " + dataB[i] + ")";          //Add console ไว้โชวคู่ผลลัพท์ที่รวมกันแล้วใกล้ tar get มากที่สุด
                    minDiff = diff;
                    if (diff == 0)
                    {
                        continue;                                                       //continue เลยไม่ต้องเช็ตข้างล่างต่อ
                    }
                }
                if (dataA[i] > target || dataB[i] > target)                             //ข้อมูลที่ใช้จับคู่ไม่มีทาง มากกว่า target แปลว่าของที่ sort มาใช้จับคู่หมดแล้ว end loop ได้เลย
                {
                    break;
                }
            }
            consoleResult += "\n\nTotal " + totalClosest + " Closest Pair  From  (listA " + dataA.Length.ToString("N0") + " Records.) ,(listB " + dataB.Length.ToString("N0") + " Records.)";
            consoleResult += "\nNumber of working loops : (" + numberOfLoop + "/" + lengthLoop + ")";
            return consoleResult;
        }

        public static string ExFindClosest(int[] A, int[] B, int X)
        {
            A = A.OrderBy(a => Math.Abs(a - ((decimal)X / 2))).ToArray();
            B = B.OrderBy(b => Math.Abs(b - ((decimal)X / 2))).ToArray();
            var result = string.Empty;
            for (int i = 0; i < X + 1; i++)
            {
                result += "(" + A[i] + " , " + B[i] + ")";
            }
            return result;
        }
    }
}
