using Xunit;
using Theatre;
using Generators;
using System.Linq;
using System.Collections.Generic;

namespace Theatre.Tests
{

    public class SeatGridTests
    {
        [Fact]
        public void Book_One()
        {
            // Arrange
            int n = 3;
            int m = 5;
            var s = new SeatGrid(n, m, m / 2, new AlphabeticalGenerator(), new NumericalGenerator());

            // Act
            s.Book(1);

            // Assert
            Assert.Equal(1, s.Rows[0].Seats.Select(x => x.Booked).Count((x) => x));
        }

        [Fact]
        public void Book_All()
        {
            // Arrange
            int n = 3;
            int m = 5;
            var s = new SeatGrid(n, m, m / 2, new AlphabeticalGenerator(), new NumericalGenerator());

            // Act
            s.Book(n * m);

            // Assert
            Assert.Equal(n * m, s.Rows.Select((r) => r.Seats.Count((t) => t.Booked)).Sum());
        }

        [Fact]
        public void Overbook()
        {
            // Arrange
            int n = 3;
            int m = 5;
            var s = new SeatGrid(n, m, m / 2, new AlphabeticalGenerator(), new NumericalGenerator());

            // Act
            var seats = s.Book(n * m + 100);

            // Assert
            Assert.Equal(n*m, seats.Length);
        }

        [Fact]
        public void Minimal_Grid_Book()
        {
            // Arrange
            var s = new SeatGrid(2, 2, 1, new AlphabeticalGenerator(), new NumericalGenerator());

            // Act
            var seats = s.Book(3);

            // Assert
            Assert.Equal(new string[] { "1", "0", "1" }, seats.Select((x) => x.ID).ToArray());
        }
    }


    public class RowTests
    {
        [Fact]
        public void Book_No_Left()
        {
            // Arrange
            var r = new Row(5, 0, "T", new NumericalGenerator());
            var seats = new List<Seat>();

            // Act
            seats.Add(r.BookNext());
            seats.Add(r.BookNext());
            seats.Add(r.BookNext());

            // Assert
            Assert.Equal(new string[]{ "2", "3", "1"}, seats.Select((x) => x.ID).ToArray());
        }

        [Fact]
        public void Book_No_Right()
        {
            // Arrange
            var r = new Row(5, 5, "T", new NumericalGenerator());
            var seats = new List<Seat>();

            // Act
            seats.Add(r.BookNext());
            seats.Add(r.BookNext());
            seats.Add(r.BookNext());

            // Assert
            Assert.Equal(new string[] { "2", "3", "1" }, seats.Select((x) => x.ID).ToArray());
        }

        [Fact]
        public void Book_Small_Right()
        {
            // Arrange
            var r = new Row(9, 7, "T", new NumericalGenerator());
            var seats = new List<Seat>();

            // Act
            seats.Add(r.BookNext());
            seats.Add(r.BookNext());
            seats.Add(r.BookNext());

            // Assert
            Assert.Equal(new string[] { "7", "8", "4" }, seats.Select((x) => x.ID).ToArray());
        }

        [Fact]
        public void Book_general_Case()
        {
            // Arrange
            int n = 42;
            var r = new Row(n, n/2, "T", new NumericalGenerator());
            var seats = new List<Seat>();

            var sIDs = new string[n];

            for (int i = n / 2; i < n; i++)
                sIDs[i-n/2] = i.ToString();

            for (int i = 0; i < n / 2; i++)
                sIDs[i+n/2] = (n / 2 - i - 1).ToString();

            // Act
            for (int i=0; i<n; i++ )
                seats.Add(r.BookNext());

            // Assert
            Assert.Equal(sIDs, seats.Select((x) => x.ID).ToArray());
        }
    }
}
