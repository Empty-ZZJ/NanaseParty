using Common;
using Common.Model.Weather;
using Common.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace OBJScripts.SettingManager
{
    public class Setting_Special : MonoBehaviour
    {
        public class _CNProvinces
        {
            public List<ProvincesItem> provinces = new();
        }
        public _CNProvinces CNProvinces;
        public Dropdown Dropdown_Province;
        public Dropdown Dropdown_City;
        public static string SelectCityEnName;

        private void Start ()
        {
            try
            {
                Dropdown_Province.ClearOptions();
                Dropdown_City.ClearOptions();
                Debug.Log(Resources.Load<TextAsset>("Config/city").text);
                CNProvinces = JsonConvert.DeserializeObject<_CNProvinces>(Resources.Load<TextAsset>("Config/city").text);
                foreach (var item in CNProvinces.provinces)
                {
                    Dropdown_Province.AddOptions(new List<string> { item.provinceName });
                }
                if (GameManager.ServerManager.Config.GameCommonConfig.GetValue("Setting", "LocalAddress") != string.Empty)
                {
                    Debug.Log("-- 已进选择过了，应该加载之前的配置信息。--");

                    var _citys = CNProvinces.provinces[Dropdown_Province.value];
                    foreach (var city in _citys.citys)
                    {
                        Dropdown_City.AddOptions(new List<string> { city.cityName });
                    }

                    Dropdown_Province.value = Convert.ToInt32(GameManager.ServerManager.Config.GameCommonConfig.GetValue("Setting", "ProvinceID"));
                    Dropdown_City.value = Convert.ToInt32(GameManager.ServerManager.Config.GameCommonConfig.GetValue("Setting", "CityID"));





                }
                else Select_City();
            }
            catch (System.Exception ex)
            {
                PopupManager.PopMessage("错误", ex.Message);
                AppLogger.Log(ex.Message, "error");
                throw;
            }
        }
        public void Select_Province ()
        {
            Dropdown_City.ClearOptions();
            var _citys = CNProvinces.provinces[Dropdown_Province.value];
            foreach (var city in _citys.citys)
            {
                Dropdown_City.AddOptions(new List<string> { city.cityName });
                //Select_City();
            }
            if (_citys.citys.Count == 1)
            {
                Select_City();
            }
        }
        public void Select_City ()
        {
            var id = CNProvinces.provinces[Dropdown_Province.value].citys[Dropdown_City.value].id;
            SelectCityEnName = id;
            Debug.Log(id);
            GameManager.ServerManager.Config.GameCommonConfig.SetValue("Setting", "LocalAddress", id);
            GameManager.ServerManager.Config.GameCommonConfig.SetValue("Setting", "ProvinceID", Dropdown_Province.value.ToString());
            GameManager.ServerManager.Config.GameCommonConfig.SetValue("Setting", "CityID", Dropdown_City.value.ToString());
        }
        public async void GetWeatherTest ()
        {
            try
            {
                var _loading = new ShowLoading("通讯中");
                var _data = await WeatherAPI.GetWeather(CNProvinces.provinces[Dropdown_Province.value].citys[Dropdown_City.value].id);
                _loading.KillLoading();
                if (_data.code != "0") throw new Exception("获取异常");
                PopupManager.PopMessage("测试", $"测试成功，该地区天气为：{_data.data.now.weather}。");
            }
            catch
            {
                PopupManager.PopMessage("错误", "获取天气错误，请检查位置选项是否正确。");
            }
        }

    }

}
