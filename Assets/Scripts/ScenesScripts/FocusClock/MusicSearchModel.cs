using System.Collections.Generic;
namespace ScenesScripts.FocusClock
{


    public class ArtistsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string picUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> alias { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long albumSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long picId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fansGroup { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string img1v1Url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long img1v1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string trans { get; set; }
    }

    public class Artist
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string picUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> alias { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long albumSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long picId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fansGroup { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string img1v1Url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long img1v1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string trans { get; set; }
    }

    public class Album
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Artist artist { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long publishTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long copyrightId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long picId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long mark { get; set; }
    }

    public class SongsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ArtistsItem> artists { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Album album { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long duration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long copyrightId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> alias { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long rtype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long ftype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long mvid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long fee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long mark { get; set; }
    }

    public class Result
    {
        /// <summary>
        /// 
        /// </summary>
        public List<SongsItem> songs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hasMore { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long songCount { get; set; }
    }

    public class MusicSearchModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Result result { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
    }
}