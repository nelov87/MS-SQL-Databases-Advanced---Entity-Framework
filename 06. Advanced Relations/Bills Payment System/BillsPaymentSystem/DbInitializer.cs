using BillsPaymentSystem.Data;
using BillsPaymentSystem.Models;
using BillsPaymentSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BillsPaymentSystem.App
{
    public class DbInitializer
    {
        public static void Seed(BillsPaymentSystemContext context)
        {
            SeedUser(context);
            SeedCreditCards(context);
            SeedBankAccount(context);
            SeedPaymentMethod(context);
        }

        private static void SeedPaymentMethod(BillsPaymentSystemContext context)
        {
            List<PaymentMethod> paymentMethods = new List<PaymentMethod>();

            for (int i = 0; i < 8; i++)
            {
                var paymentMethod = new PaymentMethod()
                {
                    UserId = new Random().Next(1, 5),
                    Type = (PaymentType)(new Random().Next(0,2))
                };

                if (i % 3 == 0)
                {
                    paymentMethod.CreditCardId = 1;
                    paymentMethod.BankAccountId = 2;
                }
                if (i % 2 == 0)
                {
                    paymentMethod.CreditCardId = 1;
                    
                }
                else
                {
                    
                    paymentMethod.BankAccountId = 2;
                }

                if (!IsValid(paymentMethod))
                {
                    continue;
                }
                paymentMethods.Add(paymentMethod);

                
            }


            context.SaveChanges();
        }

        private static void SeedBankAccount(BillsPaymentSystemContext context)
        {
            List<BankAccount> bankAccounttts = new List<BankAccount>();

            for (int i = 0; i < 8; i++)
            {
                var bankAccounttt = new BankAccount()
                {
                    Balance = new Random().Next(-5, 25000),
                    BankName = "Bank" + i,
                    SWIFT = "SWIFT" + i
                };

                if (!IsValid(bankAccounttt))
                {
                    continue;
                }
                bankAccounttts.Add(bankAccounttt);
            }

                
                context.SaveChanges();
        }

        private static void SeedCreditCards(BillsPaymentSystemContext context)
        {
            List<CreditCard> creditcards = new List<CreditCard>();

            for (int i = 0; i < 8; i++)
            {
                var creditCard = new CreditCard()
                {
                    Limit = new Random().Next(-5, 25000),
                    MoneyOwed = new Random().Next(-5, 25000),
                    ExpirationDate = DateTime.Now.AddDays(new Random().Next(-200, 200))
                };

                if (!IsValid(creditCard))
                {
                    continue;
                }

                creditcards.Add(creditCard);
                context.SaveChanges();
            }
        }

        private static void SeedUser(BillsPaymentSystemContext context)
        {
            string[] firstNames = { "Gosho", "Pesho", "Kiro", null, ""};
            string[] lastNames = { "Goshov", "Peshov", "Kirov", null, ""};
            string[] emails = { "Goshov@abv.bg", "Peshov@abv.bg", "Kirovabv.bg", null, "ERROR"};
            string[] passwords = { "Goshov@abv.bg", "Peshov@abv.bg", "Kirovabv.bg", null, "ERROR"};

            List<User> users = new List<User>();

            for (int i = 0; i < firstNames.Length; i++)
            {
                var user = new User
                {
                    FirstName = firstNames[i],
                    LastName = lastNames[i],
                    Email = emails[i],
                    Password = passwords[i]
                };

                if (!IsValid(user))
                {

                }

                users.Add(user);
            }

            context.Users.AddRange(users);
            context.SaveChanges();
        }

        private static bool IsValid(object entity)
        {
            var validationContext = new ValidationContext(entity);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entity, validationContext, validationResult, true);

            return isValid;
        }
    }
}
