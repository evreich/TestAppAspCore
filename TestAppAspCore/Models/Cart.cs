using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppAspCore.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public virtual void AddItem(Book book, int count)
        {
            CartLine line = lineCollection.Where(p => p.Id == book.Id).FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(
                    new CartLine
                    {
                        Id = book.Id,
                        Book = book,
                        Count = count
                    }
                );
            }
            else
            {
                line.Count += count;
            }
        }

        public virtual void RemoveLine(Book book) => lineCollection.RemoveAll(l => l.Book.Id == book.Id);
        public virtual int ComputeTotalValue() => lineCollection.Count;
        public virtual void Clear() => lineCollection.Clear();
        public virtual IEnumerable<CartLine> Lines => lineCollection;
    }

    public class CartLine
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public int Count { get; set; }
    }
}
