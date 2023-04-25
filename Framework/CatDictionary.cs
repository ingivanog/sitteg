using GuanajuatoAdminUsuarios.Framework.Catalogs;
using GuanajuatoAdminUsuarios.Interfaces;
using GuanajuatoAdminUsuarios.Models;
using GuanajuatoAdminUsuarios.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GuanajuatoAdminUsuarios.Framework
{
    public class CatDictionary
    {
        private readonly IPadronDepositosGruasService _padronDepositosGruasService;
        private readonly IGruasService _gruasService;
        private readonly IMunicipiosService _municipiosService;
        private readonly IConcesionariosService _concesionariosService;


        public CatDictionary(IPadronDepositosGruasService padronDepositosGruasService,
             IGruasService gruasService, IMunicipiosService municipiosService, IConcesionariosService concesionariosService
            )
        {
            _padronDepositosGruasService = padronDepositosGruasService;
            _gruasService = gruasService;
            _municipiosService = municipiosService;
            _concesionariosService = concesionariosService;
        }
        /// <summary>
        /// Obtiene la información de los catálogos internos (no se guarda en la base de datos)
        /// </summary>
        /// <typeparam name="TEnum">Nombre de catálogo</typeparam>
        /// <returns></returns>
        public Dictionary<int, string> GetCatalogSystem<TEnum>()
        {
            Type enumType = typeof(TEnum);
            return (from int e in Enum.GetValues(typeof(TEnum))
                    select new
                    {
                        Id = e,
                        Name = ((Enum)Enum.ToObject(enumType, e)).GetDescription()
                    }).ToDictionary(item => item.Id, item => item.Name);
        }
        /// <summary>
        /// Obtiene la información de los catálogos internos (no se guarda en la base de datos)
        /// </summary>
        /// <param name="value">Nombre de catálogo</param>
        /// <returns></returns>
        public SystemCatalogModel GetCatalogSystem(string name)
        {
            Type enumType = typeof(CatEnumerator).GetNestedType(name);
            if (enumType != null)
                return new SystemCatalogModel()
                {
                    CatalogName = name,
                    CatalogList = (from int e in Enum.GetValues(enumType)
                                   select new SystemCatalogListModel
                                   {
                                       Id = e,
                                       Text = ((Enum)Enum.ToObject(enumType, e)).GetDescription()
                                   }).ToList()
                };
            else
                return null;
        }

        public SystemCatalogModel GetCatalog(string catalog, string parameter)
        {
            SystemCatalogModel catalogModel = new SystemCatalogModel();
            Guid GuidId;
            int intId;
            long longId;
            switch (catalog)
            {
                case "CatConcesionariosByIdDelegacion":
                    if (int.TryParse(parameter, out intId))
                    {
                        catalogModel.CatalogName = catalog;
                        catalogModel.CatalogList = _concesionariosService.GetConcesionarios2ByIdDelegacion(intId)
                                .Select(s => new SystemCatalogListModel()
                                {
                                    Id = s.idConcesionario,
                                    Text = s.nombre
                                }).ToList();
                    }
                    break;
                case "CatTiposGrua":
                    catalogModel.CatalogName = catalog;
                    catalogModel.CatalogList = _gruasService.GetTipoGruas()
                            .Select(s => new SystemCatalogListModel()
                            {
                                Id = s.IdTipoGrua,
                                Text = s.TipoGrua
                            }).ToList();
                    break;
                default:
                    return catalogModel;
            }
            return catalogModel;
        }
    }
}
