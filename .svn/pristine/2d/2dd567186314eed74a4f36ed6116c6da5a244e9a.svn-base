using GuanajuatoAdminUsuarios.Framework.Catalogs;
using GuanajuatoAdminUsuarios.Interfaces;
using GuanajuatoAdminUsuarios.Models;
using GuanajuatoAdminUsuarios.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GuanajuatoAdminUsuarios.Framework
{
    public class CatDictionary : ICatDictionary
    {
        private readonly IPadronDepositosGruasService _padronDepositosGruasService;
        private readonly IGruasService _gruasService;
        private readonly IMunicipiosService _municipiosService;
        private readonly IConcesionariosService _concesionariosService;
        private readonly ICatalogosService _catalogosService;


        public CatDictionary(IPadronDepositosGruasService padronDepositosGruasService,
                             IGruasService gruasService, 
                             IMunicipiosService municipiosService, 
                             IConcesionariosService concesionariosService,
                             ICatalogosService catalogosService)
        {
            _padronDepositosGruasService = padronDepositosGruasService;
            _gruasService = gruasService;
            _municipiosService = municipiosService;
            _concesionariosService = concesionariosService;
            _catalogosService = catalogosService;
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
            string[] campos;
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
                case "CatMunicipios":
                    catalogModel.CatalogName = catalog;
                    campos = new string[] { "idMunicipio", "municipio" };
                    catalogModel.CatalogList = _catalogosService.GetGenericCatalogos("catMunicipios", campos)
                            .Select(s => 
                            new SystemCatalogListModel()
                            {
                                Id = Convert.ToInt32(s["idMunicipio"]),
                                Text = Convert.ToString(s["municipio"])
                            }).ToList();
                    break;
                case "CatDelegaciones":
                    catalogModel.CatalogName = catalog;
                    campos = new string[] { "idDelegacion", "delegacion" };
                    catalogModel.CatalogList = _catalogosService.GetGenericCatalogos("catDelegaciones", campos)
                            .Select(s =>
                            new SystemCatalogListModel()
                            {
                                Id = Convert.ToInt32(s["idDelegacion"]),
                                Text = Convert.ToString(s["delegacion"])
                            }).ToList();
                    break;
                case "CatResponsablesPensiones":
                    catalogModel.CatalogName = catalog;
                    campos = new string[] { "idResponsable", "responsable" };
                    catalogModel.CatalogList = _catalogosService.GetGenericCatalogos("catResponsablePensiones", campos)
                            .Select(s =>
                            new SystemCatalogListModel()
                            {
                                Id = Convert.ToInt32(s["idResponsable"]),
                                Text = Convert.ToString(s["responsable"])
                            }).ToList();
                    break;
                case "CatClasificacionGruas":
                    catalogModel.CatalogName = catalog;
                    campos = new string[] { "idClasificacionGrua", "clasificacion" };
                    catalogModel.CatalogList = _catalogosService.GetGenericCatalogos("catClasificacionGruas", campos)
                            .Select(s =>
                            new SystemCatalogListModel()
                            {
                                Id = Convert.ToInt32(s["idClasificacionGrua"]),
                                Text = Convert.ToString(s["clasificacion"])
                            }).ToList();
                    break;
                case "CatSituacionGruas":
                    catalogModel.CatalogName = catalog;
                    campos = new string[] { "idSituacion", "situacion" };
                    catalogModel.CatalogList = _catalogosService.GetGenericCatalogos("catSituacionGruas", campos)
                            .Select(s =>
                            new SystemCatalogListModel()
                            {
                                Id = Convert.ToInt32(s["idSituacion"]),
                                Text = Convert.ToString(s["situacion"])
                            }).ToList();
                    break;
                default:
                    return catalogModel;
            }
            return catalogModel;
        }
    }
}
