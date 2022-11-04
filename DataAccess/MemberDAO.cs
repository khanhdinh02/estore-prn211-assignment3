using BusinessObject;
using System.Diagnostics.Metrics;

namespace DataAccess
{
    public class MemberDAO
    {
        private static MemberDAO? instance;
        private static readonly object instanceLock = new object();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    instance ??= new MemberDAO();
                    return instance;
                }
            }
        }

        public IEnumerable<Member> GetAll()
        {
            using var context = new FStoreContext();
            List<Member> list = context.Members.ToList();
            return list;
        }

        public Member? GetById(int id)
        {
            using var context = new FStoreContext();
            Member? member = context.Members.SingleOrDefault(m => m.MemberId == id);
            return member;
        }

        public Member? GetByEmail(string email)
        {
            using var context = new FStoreContext();
            Member? member = context.Members
                .SingleOrDefault(m => m.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            return member;
        }

        public void Add(Member member)
        {
            if (GetById(member.MemberId) != null)
                throw new Exception("Member has existed");
            using var context = new FStoreContext();
            context.Members.Add(member);
            context.SaveChanges();
        }

        public void Update(Member member)
        {
            if (GetById(member.MemberId) == null)
                throw new Exception("Member does not exist");
            using var context = new FStoreContext();
            context.Members.Update(member);
            context.SaveChanges();
        }

        public void Remove(int id)
        {
            Member? member = GetById(id);
            if (member == null)
                throw new Exception("Member does not exist");
            using var context = new FStoreContext();
            context.Members.Remove(member);
            context.SaveChanges();
        }
    }
}