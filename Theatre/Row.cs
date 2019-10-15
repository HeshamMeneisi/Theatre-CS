using Generators;
using System;
using System.Text;

namespace Theatre
{
    public class Row
    {
        int nextCenterRight, nextCenterLeft, nextXRight, rightStart;
        double mid;
        uint bookedCount = 0;

        public Row(int nSeats, int rightStart, string id, IDGenerator cell_id_gen)
        {
            ID = id;
            mid = (double)(nSeats - 1) / 2;
            this.rightStart = rightStart;
            nextXRight = rightStart;
            nextCenterRight = (int)Math.Ceiling(mid);
            nextCenterLeft = nextCenterRight - 1;
            Seats = new Seat[nSeats];
            for(int i = 0; i < Seats.Length; i++)
            {
                Seats[i] = new Seat(cell_id_gen.Next(), this, i < rightStart ? Orientation.Left : Orientation.Right);
            }
        }

        public string ID { get; private set; }
        public bool AllBooked { get { return bookedCount >= Seats.Length; } }
        public Seat[] Seats { get; }

        Seat GetNextCenterSeat()
        {
            Seat seat = null;
            // Choose closest to center, unless taken (right priority)
            if (
                (int)Math.Abs(nextCenterLeft - mid) >= (int)Math.Abs(nextCenterRight - mid) &&
                nextCenterRight < Seats.Length &&
                !Seats[nextCenterRight].Booked
                )
            {
                seat = Seats[nextCenterRight];
                nextCenterRight += 1;
            }
            else if (nextCenterLeft >= 0)
            {
                seat = Seats[nextCenterLeft];
                nextCenterLeft -= 1;
            }

            return seat;
        }

        public Seat BookNext()
        {
            if (AllBooked) return null;

            Seat seat = null;

            // If left section is bigger than right
            if (
                nextCenterRight < rightStart &&
                nextXRight < Seats.Length &&
                !Seats[nextXRight].Booked
               )
            {
                // Must fill the whole right section first
                seat = Seats[nextXRight];                
                nextXRight += 1;
            }
            // If left section is empty/non-existent, or right side is full/non-existent
            else if (nextCenterLeft >= rightStart || nextXRight >= Seats.Length)
            {
                // Get closest to center regardless of section
                seat = GetNextCenterSeat();
            }
            // Otherwise, fill right
            else if (nextCenterRight < Seats.Length)
            {
                seat = Seats[nextCenterRight];
                nextCenterRight += 1;
            }
            // Then left
            else if (nextCenterLeft >= 0)
            {
                seat = Seats[nextCenterLeft];
                nextCenterLeft -= 1;
            }

            seat.Book();
            bookedCount += 1;
            return seat;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ID + "|\t");
            foreach (var seat in Seats)
            {                
                sb.Append(seat.Orientation == Orientation.Left ? "<" : "");
                sb.Append(seat.Booked ? "X" : "O");
                sb.Append(seat.Orientation == Orientation.Right ? ">\t" : "\t");
            }
            return sb.ToString();
        }
    }
}
