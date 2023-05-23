﻿using Microsoft.AspNetCore.Mvc;
using AppData.IRepositories;
using AppData.Repositories;
using AppData.Models;
using Microsoft.EntityFrameworkCore;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private readonly IAllRepository<SanPham> repos;
        AssignmentDBContext context = new AssignmentDBContext();
        public SanPhamController()
        {
            repos = new AllRepository<SanPham>(context, context.SanPhams);
        }
        // GET: api/<SanPhamController>
        [HttpGet]
        public List<SanPham> Get()
        {
            return repos.GetAll();
        }

        // GET api/<SanPhamController>/5
        [HttpGet("{name}")]
        public List<SanPham> Get(string name)
        {
            return repos.GetAll().Where(x=>x.Ten.Contains(name)).ToList();
        }

        // POST api/<SanPhamController>
        [HttpPost("create-san-pham")]
        public bool Post(string ten,Guid idLoaiSP,string moTa)
        {
            SanPham sanPham = new SanPham();
            sanPham.ID = Guid.NewGuid();
            sanPham.Ten = ten;
            sanPham.IDLoaiSP = idLoaiSP;
            sanPham.MoTa = moTa;
            sanPham.TrangThai = 1;
            return repos.Add(sanPham);
        }

        // PUT api/<SanPhamController>/5
        [HttpPut("{id}")]
        public bool Put(Guid id, string ten, Guid idLoaiSP, string moTa)
        {
            var sp = repos.GetAll().FirstOrDefault(x=>x.ID == id);
            if(sp != null)
            {
                sp.Ten = ten;
                sp.IDLoaiSP = idLoaiSP;
                sp.MoTa = moTa;
                return repos.Update(sp);
            }
            else
            {
                return false;
            }
        }

        // DELETE api/<SanPhamController>/5
        [HttpDelete("{id}")]
        public bool Delete(Guid id)
        {
            var sp = repos.GetAll().FirstOrDefault(x => x.ID == id);
            if (sp != null)
            {
                return repos.Delete(sp);
            }
            else
            {
                return false;
            }
        }
    }
}
