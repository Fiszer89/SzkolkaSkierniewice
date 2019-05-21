using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using System.Web.Mvc;
using Moq;
using SzkolkaSkierniewice.Domain.Abstract;
using SzkolkaSkierniewice.Domain.Entities;
using SzkolkaSkierniewice.Domain.Concrete;
using SzkolkaSkierniewice.Infrastructure.Abstract;
using SzkolkaSkierniewice.Infrastructure.Concrete;

namespace SzkolkaSkierniewice.Infrastructure
{
    public class NinjectDependencyResolver:IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParm)
        {
            kernel = kernelParm;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            /*
            Mock<IDrzewoAlejoweRepository> mock = new Mock<IDrzewoAlejoweRepository>();
            mock.Setup(m => m.DrzewaAlejowe).Returns(new List<DrzewoAlejowe>
            {
                new DrzewoAlejowe{ Available = true,  Discount = 0.3m, Quantity = 5, Description = "", Date = DateTime.Now, Name = "tuja", Price = 300, WidthMax = 200, WidthMin = 100, ProductID = 1},
                new DrzewoAlejowe{ Available = true,  Discount = 0.3m, Quantity = 5, Description = "", Date = DateTime.Now, Name = "tuja2", Price = 300, WidthMax = 200, WidthMin = 100,  ProductID = 2},
                new DrzewoAlejowe{ Available = true,  Discount = 0.3m, Quantity = 5, Description = "", Date = DateTime.Now, Name = "tuja3", Price = 300, WidthMax = 200, WidthMin = 100,  ProductID = 3},
                new DrzewoAlejowe{ Available = true,  Discount = 0.3m, Quantity = 5, Description = "", Date = DateTime.Now, Name = "tuja4", Price = 300, WidthMax = 200, WidthMin = 100},
                new DrzewoAlejowe{ Available = true,  Discount = 0.3m, Quantity = 5, Description = "", Date = DateTime.Now, Name = "tuja5", Price = 300, WidthMax = 200, WidthMin = 100},
                new DrzewoAlejowe{ Available = true,  Discount = 0.3m, Quantity = 5, Description = "", Date = DateTime.Now, Name = "tuja6", Price = 300, WidthMax = 200, WidthMin = 100}
            });

            kernel.Bind<IDrzewoAlejoweRepository>().ToConstant(mock.Object);

            Mock<IIglakGruntRepository> mock2 = new Mock<IIglakGruntRepository>();
            mock2.Setup(m => m.IglakiGrunt).Returns(new List<IglakGrunt>
            {
                new IglakGrunt{ Available = true,  Discount = 0.3m, Quantity = 5, Description = "", Date = DateTime.Now, Name = "abc", Price = 300},
                new IglakGrunt{ Available = true,  Discount = 0.3m, Quantity = 5, Description = "", Date = DateTime.Now, Name = "asd", Price = 300},
                new IglakGrunt{ Available = true,  Discount = 0.3m, Quantity = 5, Description = "", Date = DateTime.Now, Name = "tua3", Price = 300},
                new IglakGrunt{ Available = true,  Discount = 0.3m, Quantity = 5, Description = "", Date = DateTime.Now, Name = "tudqwdja4", Price = 300},
                new IglakGrunt{ Available = true,  Discount = 0.3m, Quantity = 5, Description = "", Date = DateTime.Now, Name = "t122", Price = 300},
                new IglakGrunt{ Available = true,  Discount = 0.3m, Quantity = 5, Description = "", Date = DateTime.Now, Name = "tu326", Price = 300}
            });

            kernel.Bind<IIglakGruntRepository>().ToConstant(mock2.Object);

            Mock<IIglakPojemnikRepository> mock3 = new Mock<IIglakPojemnikRepository>();
            mock3.Setup(m => m.IglakiPojemnik).Returns(new List<IglakPojemnik>
            {
                new IglakPojemnik{ Available = true,  Discount = 0.3m, Quantity = 5, Description = "", Date = DateTime.Now, Name = "abc", Price = 300},
                new IglakPojemnik{ Available = true,  Discount = 0.3m, Quantity = 5, Description = "", Date = DateTime.Now, Name = "asd", Price = 300},
                new IglakPojemnik{ Available = true,  Discount = 0.3m, Quantity = 5, Description = "", Date = DateTime.Now, Name = "tua3", Price = 300},
                new IglakPojemnik{ Available = true,  Discount = 0.3m, Quantity = 5, Description = "", Date = DateTime.Now, Name = "tudqwdja4", Price = 300},
                new IglakPojemnik{ Available = true,  Discount = 0.3m, Quantity = 5, Description = "", Date = DateTime.Now, Name = "t122", Price = 300},
                new IglakPojemnik{ Available = true,  Discount = 0.3m, Quantity = 5, Description = "", Date = DateTime.Now, Name = "tu326", Price = 300}
            });

            kernel.Bind<IIglakPojemnikRepository>().ToConstant(mock3.Object);

            Mock<IKrzewLisciastyRepository> mock4 = new Mock<IKrzewLisciastyRepository>();
            mock4.Setup(m => m.KrzewyLisciaste).Returns(new List<KrzewLisciasty>
            {
                new KrzewLisciasty{ProductID = 1 ,Available = true,  Discount = 0.3m, Quantity = 5, Description = "", Date = DateTime.Now, Name = "abc", Price = 300, Boxes = new List<Box>()},
                new KrzewLisciasty{ProductID = 2 ,Available = true,  Discount = 0.3m, Quantity = 5, Description = "", Date = DateTime.Now, Name = "asd", Price = 300, Boxes = new List<Box>()},
                new KrzewLisciasty{ProductID = 3 ,Available = true,  Discount = 0.3m, Quantity = 5, Description = "", Date = DateTime.Now, Name = "tua3", Price = 300, Boxes = new List<Box>()},
                new KrzewLisciasty{ProductID = 4 ,Available = true,  Discount = 0.3m, Quantity = 5, Description = "", Date = DateTime.Now, Name = "tudqwdja4", Price = 300, Boxes = new List<Box>()},
                new KrzewLisciasty{ProductID = 5 ,Available = true,  Discount = 0.3m, Quantity = 5, Description = "", Date = DateTime.Now, Name = "t122", Price = 300, Boxes = new List<Box>()},
                new KrzewLisciasty{ProductID = 6 ,Available = true,  Discount = 0.3m, Quantity = 5, Description = "", Date = DateTime.Now, Name = "tu326", Price = 300, Boxes = new List<Box>()}
            });

            kernel.Bind<IKrzewLisciastyRepository>().ToConstant(mock4.Object);

            Mock<IBoxRepository> mock5 = new Mock<IBoxRepository>();
            mock5.Setup(m => m.Boxes).Returns(new List<Box>
            {
                new Box{ Name = "C3"},
                new Box{ Name = "C4"}
            });

            kernel.Bind<IBoxRepository>().ToConstant(mock5.Object);
            */
            kernel.Bind<IDrzewoAlejoweRepository>().To<EFDrzewoAlejoweRepository>();
            kernel.Bind<IIglakGruntRepository>().To<EFIglakGruntRepository>();
            kernel.Bind<IIglakPojemnikRepository>().To<EFIglakPojemnikRepository>();
            kernel.Bind<IKrzewLisciastyRepository>().To<EFKrzewLisciastyRepository>();
            kernel.Bind<IBoxRepository>().To<EFBoxRepository>();
            kernel.Bind<IPromotionRepository>().To<EFPromotionRepository>();
            kernel.Bind<IProductRepository>().To<EFProductRepository>();
            kernel.Bind<IGalleryImageRepository>().To<EFGalleryImageRepositroy>();

            kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }
    }
}