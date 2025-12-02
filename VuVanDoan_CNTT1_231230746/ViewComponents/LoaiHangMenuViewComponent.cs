//using Ktralan2_02.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace ktra02.ViewComponents
//{
//    public class LoaiHangMenuViewComponent: Microsoft.AspNetCore.Mvc.ViewComponent
//    {
//        private readonly QlhangHoaContext _context;

//        public LoaiHangMenuViewComponent(QlhangHoaContext context)
//        {
//            _context = context;
//        }

//        public IViewComponentResult Invoke()
//        {
//            var loaihang = _context.LoaiHangs.OrderBy(x => x.TenLoai).ToList();
//            return View(loaihang);
//        }
//    }
//}
