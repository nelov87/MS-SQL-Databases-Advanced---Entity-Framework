namespace BookShop
{
    using BookShop.Models;
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                //DbInitializer.ResetDatabase(db);

                // 01
                //var result = GetBooksByAgeRestriction(db, "teEN");

                //02
                //var result = GetGoldenBooks(db);

                //03
                //var result = GetBooksByPrice(db);

                //04
                //var result = GetBooksNotReleasedIn(db, 1998);

                //05
                //var result = GetBooksByCategory(db, "horror mystery drama");

                //06
                //var result = GetBooksReleasedBefore(db, "12-04-1992");

                //07
                //var result = GetAuthorNamesEndingIn(db, "e");

                //08
                //var result = GetBookTitlesContaining(db, "sK");

                //09
                //var result = GetBooksByAuthor(db, "R");

                //10
                //var result = CountBooks(db, 40);

                //11
                //var result = CountCopiesByAuthor(db);

                //12
                //var result = GetTotalProfitByCategory(db);

                //13
                //var result = GetMostRecentBooks(db);

                //14
                IncreasePrices(db);
                
            }
        }

        public static int RemoveBooks(BookShopContext context)
        {
            var booksForDelete = context.Books
                .Where(b => b.Copies < 4200)
                .ToList();

            context.RemoveRange(booksForDelete);
            context.SaveChanges();

            //return context.SaveChanges(); not working in judge

            return booksForDelete.Count;
        }


        public static void IncreasePrices(BookShopContext context)
        {
            context.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .ToList()
                .ForEach(b => b.Price += 5);

            context.SaveChanges();
        }


        public static string GetMostRecentBooks(BookShopContext context)
        {
            var categoriesWithBooks = context.Categories
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    c.Name,
                    Books = c.CategoryBooks
                        .Select(cb => cb.Book)
                        .OrderByDescending(b => b.ReleaseDate)
                        .Take(3)
                })
                .ToList();

            return String.Join(Environment.NewLine,
                categoriesWithBooks
                .Select(c => $"--{c.Name}{Environment.NewLine}{String.Join(Environment.NewLine, c.Books.Select(b => $"{b.Title} ({b.ReleaseDate.Value.Year})"))}"));
        }


        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var profitsByCategory = context.Categories
                .Select(c => new
                {
                    c.Name,
                    Profit = c.CategoryBooks.Select(cb => cb.Book.Copies * cb.Book.Price).Sum()
                })
                .OrderByDescending(c => c.Profit)
                .ThenBy(c => c.Name)
                .ToList();

            return String.Join(Environment.NewLine, profitsByCategory.Select(p => $"{p.Name} ${p.Profit:f2}"));
        }


        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var copiesByAuthor = context.Authors
                .Select(a => new
                {
                    Name = $"{a.FirstName} {a.LastName}",
                    Copies = a.Books.Select(b => b.Copies).Sum()
                })
                .OrderByDescending(a => a.Copies)
                .ToList();

            return String.Join(Environment.NewLine, copiesByAuthor.Select(c => $"{c.Name} - {c.Copies}"));
        }


        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var books = context.Books
                .Count(b => b.Title.Length > lengthCheck);
            return books;
        }


        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId)
                .Select(a => new
                {
                    a.Title,
                    AuthorName = a.Author.FirstName + " " + a.Author.LastName
                })
                .ToList();

            return string.Join(Environment.NewLine, books.Select(b => $"{b.Title} ({b.AuthorName})"));
                
        }


        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(b => b.Title)
                .OrderBy(t => t)
                .ToList();

            return string.Join(Environment.NewLine, books);
        }


        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var books = context.Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => new
                {
                    FullName = a.FirstName + " " + a.LastName
                })
                .OrderBy(n => n.FullName)
                .ToList();

            return string.Join(Environment.NewLine, books.Select(n => $"{n.FullName}"));
        }


        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            DateTime dt;
            DateTime.TryParseExact(date, "dd-MM-yyyy",
                          CultureInfo.InvariantCulture,
                          DateTimeStyles.None, out dt);

            var books = context.Books
                .Where(d => d.ReleaseDate < dt)
                .OrderByDescending(d => d.ReleaseDate)
                .Select(a => new
                {
                    a.Title,
                    a.EditionType,
                    a.Price
                })
                .ToList();

            return string.Join(Environment.NewLine, books.Select(a => $"{a.Title} - {a.EditionType} - ${a.Price:F2}"));
                
        }


        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var category = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(a => a.ToLower()).ToList();

            var books = context.Books
                .Where(b => b.BookCategories
                    .Any(bc => category.Contains(bc.Category.Name.ToLower())))
                    .Select(b => b.Title)
                    .OrderBy(x => x)
                    .ToList();

            return string.Join(Environment.NewLine, books);
        }


        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            DateTime dt;
            DateTime.TryParseExact(year.ToString(), "yyyy",
                          CultureInfo.InvariantCulture,
                          DateTimeStyles.None, out dt);

            var books = context.Books
                .Where(y => y.ReleaseDate.Value.Year != dt.Year)
                .OrderBy(b => b.BookId)
                .Select(t => t.Title)
                .ToList();

            return string.Join(Environment.NewLine, books);
        }


        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(p => p.Price > 40)
                .OrderByDescending(p => p.Price)
                .Select(a => new
                {
                    a.Title,
                    a.Price
                })
                .ToList();

            var sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - ${book.Price:F2}");
            }

            return sb.ToString().TrimEnd();
        }


        public static string GetGoldenBooks(BookShopContext context)
        {
            var golden = Enum.Parse<EditionType>("Gold", true);

            var goldenEditionsBooks = context.Books
                .Where(e => e.EditionType == golden && e.Copies < 5000)
                .OrderBy(x => x.BookId)
                .Select(t => t.Title)
                .ToList();

            var result = string.Join(Environment.NewLine, goldenEditionsBooks);

            return result;
        }


        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var ageRestriction = Enum.Parse<AgeRestriction>(command, true);

            var books = context.Books
                .Where(a => a.AgeRestriction == ageRestriction)
                .Select(t => t.Title)
                .OrderBy(x => x)
                .ToList();

            var result = string.Join(Environment.NewLine, books);
            return result;
        }
    }
}
