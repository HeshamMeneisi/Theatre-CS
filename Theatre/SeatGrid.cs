using System.Collections.Generic;
using System.Text;
using Generators;

namespace Theatre
{
    public class SeatGrid
    {
        uint currentRow;
        int perRow;

        public SeatGrid(int nRows, int perRow, int rightStart, IDGenerator rowIDGen, IDGenerator seatIDGen)
        {
            Rows = new Row[nRows];
            AllBooked = false;
            currentRow = 0;
            this.perRow = perRow;

            for (var i = 0; i < Rows.Length; i++)
            {
                seatIDGen.Reset();
                Rows[i] = new Row(perRow, rightStart, rowIDGen.Next(), seatIDGen);                
            }
        }

        public bool AllBooked { get; private set; }
        public Row[] Rows { get; }

        void MoveToNextRow()
        {
            // Back to front (0 => len)
            currentRow += 1;
            if (currentRow >= Rows.Length)
            {
                AllBooked = true;
            }
        }

        public Seat[] Book(int n_seats)
        {
            var booked_seats = new List<Seat>();

            if (AllBooked) return null;

            for (var i = 0; i < n_seats & !AllBooked; i++)
            {
                var next = Rows[currentRow].BookNext();                
                booked_seats.Add(next);             

                if (Rows[currentRow].AllBooked)
                {
                    // Can set AllBooked and exit loop on next iteration (if last row)
                    MoveToNextRow();
                }
            }

            return booked_seats.ToArray();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var row in Rows)
            {
                sb.Append(row.ToString());
                sb.AppendLine();
            }
            sb.AppendLine();
            sb.Append("| < Left | > Right | O Empty | X Booked |");
            return sb.ToString();
        }
    }
}
