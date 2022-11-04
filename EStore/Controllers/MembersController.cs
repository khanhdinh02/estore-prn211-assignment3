using BusinessObject;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EStore.Controllers
{
    public class MembersController : Controller
    {
        IMemberRepository memberRepository = new MemberRepository();

        // GET: MembersController
        public ActionResult Index()
        {
            return View(memberRepository.GetAll());
        }

        // GET: MembersController/Details/5
        public ActionResult Details(int id)
        {
            var member = memberRepository.GetById(id);
            if (member == null)
                return NotFound();
            return View(member);
        }

        // GET: MembersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MembersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Member member)
        {
            try
            {
                if (ModelState.IsValid)
                    memberRepository.Insert(member);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: MembersController/Edit/5
        public ActionResult Edit(int id)
        {
            var member = memberRepository.GetById(id);
            if (member == null)
                return NotFound();
            return View(member);
        }

        // POST: MembersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Member member)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    member.MemberId = id;
                    memberRepository.Update(member);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: MembersController/Delete/5
        public ActionResult Delete(int id)
        {
            var member = memberRepository.GetById(id);
            if (member == null)
                return NotFound();
            return View(member);
        }

        // POST: MembersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Member member)
        {
            try
            {
                memberRepository.Remove(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}
