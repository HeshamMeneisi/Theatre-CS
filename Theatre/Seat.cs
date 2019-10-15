namespace Theatre
{
    public class Seat
    {
        Row row;

        public Seat(string id, Row row, Orientation orientation)
        {
            ID = id;
            Booked = false;
            Orientation = orientation;
            this.row = row;
        }

        public string ID { get; }
        public bool Booked { get; set; }
        public Orientation Orientation { get; }

        public void Book()
        {
            Booked = true;
        }

        public override string ToString()
        {
            return row.ID + ":" + ID;
        }
    }

    public enum Orientation
    {
        Left, Right
    }
}
