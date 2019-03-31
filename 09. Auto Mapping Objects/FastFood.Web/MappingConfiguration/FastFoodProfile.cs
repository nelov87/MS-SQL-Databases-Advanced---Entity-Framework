namespace FastFood.Web.MappingConfiguration
{
    using AutoMapper;
    using FastFood.Web.ViewModels.Categories;
    using FastFood.Web.ViewModels.Employees;
    using FastFood.Web.ViewModels.Items;
    using FastFood.Web.ViewModels.Orders;
    using Models;

    using ViewModels.Positions;

    public class FastFoodProfile : Profile
    {
        public FastFoodProfile()
        {
            //Positions
            this.CreateMap<CreatePositionInputModel, Position>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.PositionName));

            this.CreateMap<Position, PositionsAllViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));

            //employees

            this.CreateMap<Position, RegisterEmployeeViewModel>()
                .ForMember(p => p.PositionName, pos => pos.MapFrom(i => i.Name));

            this.CreateMap<RegisterEmployeeInputModel, Employee>();

            this.CreateMap<Employee, EmployeesAllViewModel>()
                .ForMember(x => x.Position, y => y.MapFrom(s => s.Position.Name));

            //Category
            this.CreateMap<CreateCategoryInputModel, Category>()
                .ForMember(c => c.Name, x => x.MapFrom(n => n.CategoryName));

            //Items

            this.CreateMap<Category, CreateItemViewModel>()
                .ForMember(c => c.CategoryName, s => s.MapFrom(i => i.Name));

            this.CreateMap<CreateItemInputModel, Item>();
                

            this.CreateMap<Item, ItemsAllViewModels>();

            this.CreateMap<Item, ItemsAllViewModels>()
                 .ForMember(x => x.Category, y => y.MapFrom(c => c.Category.Name));

            //Orders
            this.CreateMap<CreateOrderInputModel, Order>();

            this.CreateMap<Order, OrderAllViewModel>()
                .ForMember(x => x.OrderId, o => o.MapFrom(i => i.Id))
                .ForMember(x => x.Employee, y => y.MapFrom(e => e.Employee.Name))
                .ForMember(x => x.DateTime, y => y.MapFrom(s => s.DateTime.ToString("g")));

            //this.CreateMap<Order, OrderAllViewModel>()
            //    .ForMember(x => x.Employee, y => y.MapFrom(e => e.Employee.Name));
        }
    }
}
