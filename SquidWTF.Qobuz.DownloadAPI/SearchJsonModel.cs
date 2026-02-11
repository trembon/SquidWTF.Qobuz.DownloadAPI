namespace SquidWTF.Qobuz.DownloadAPI;

public class SearchJsonModel
{
    public bool success { get; set; }
    public Data data { get; set; }
}

public class Data
{
    //public string query { get; set; }
    public Albums albums { get; set; }
    //public Tracks tracks { get; set; }
    //public Artists artists { get; set; }
    //public Playlists playlists { get; set; }
    //public Stories stories { get; set; }
    //public Most_Popular most_popular { get; set; }
    //public object switchTo { get; set; }
}

public class Albums
{
    //public int limit { get; set; }
    //public int offset { get; set; }
    //public int total { get; set; }
    public Item[] items { get; set; }
}

public class Item
{
    //public int maximum_bit_depth { get; set; }
    //public Image image { get; set; }
    //public int media_count { get; set; }
    public Artist artist { get; set; }
    //public Artist1[] artists { get; set; }
    //public string upc { get; set; }
    //public int released_at { get; set; }
    //public Label label { get; set; }
    public string title { get; set; }
    //public int qobuz_id { get; set; }
    //public string version { get; set; }
    //public string url { get; set; }
    //public string slug { get; set; }
    //public int duration { get; set; }
    public bool parental_warning { get; set; }
    //public int popularity { get; set; }
    public int tracks_count { get; set; }
    //public Genre genre { get; set; }
    //public int maximum_channel_count { get; set; }
    public string id { get; set; }
    //public float maximum_sampling_rate { get; set; }
    //public object[] articles { get; set; }
    public string release_date_original { get; set; }
    //public string release_date_download { get; set; }
    //public string release_date_stream { get; set; }
    //public bool purchasable { get; set; }
    //public bool streamable { get; set; }
    //public bool previewable { get; set; }
    //public bool sampleable { get; set; }
    //public bool downloadable { get; set; }
    //public bool displayable { get; set; }
    //public int? purchasable_at { get; set; }
    //public int streamable_at { get; set; }
    //public bool hires { get; set; }
    //public bool hires_streamable { get; set; }
}

public class Image
{
    public string small { get; set; }
    public string thumbnail { get; set; }
    public string large { get; set; }
    public object back { get; set; }
}

public class Artist
{
    public object image { get; set; }
    public string name { get; set; }
    public int id { get; set; }
    public int albums_count { get; set; }
    public string slug { get; set; }
    public object picture { get; set; }
}

public class Label
{
    public string name { get; set; }
    public int id { get; set; }
    public int albums_count { get; set; }
    public int supplier_id { get; set; }
    public string slug { get; set; }
}

public class Genre
{
    public int[] path { get; set; }
    public string color { get; set; }
    public string name { get; set; }
    public int id { get; set; }
    public string slug { get; set; }
}

public class Artist1
{
    public int id { get; set; }
    public string name { get; set; }
    public string[] roles { get; set; }
}

public class Tracks
{
    public int limit { get; set; }
    public int offset { get; set; }
    public int total { get; set; }
    public Item1[] items { get; set; }
}

public class Item1
{
    public int maximum_bit_depth { get; set; }
    public string copyright { get; set; }
    public string performers { get; set; }
    public Audio_Info audio_info { get; set; }
    public Performer performer { get; set; }
    public Album album { get; set; }
    public object work { get; set; }
    public Composer composer { get; set; }
    public string isrc { get; set; }
    public string title { get; set; }
    public string version { get; set; }
    public int duration { get; set; }
    public bool parental_warning { get; set; }
    public int track_number { get; set; }
    public int maximum_channel_count { get; set; }
    public int id { get; set; }
    public int media_number { get; set; }
    public float maximum_sampling_rate { get; set; }
    public string release_date_original { get; set; }
    public string release_date_download { get; set; }
    public string release_date_stream { get; set; }
    public string release_date_purchase { get; set; }
    public bool purchasable { get; set; }
    public bool streamable { get; set; }
    public bool previewable { get; set; }
    public bool sampleable { get; set; }
    public bool downloadable { get; set; }
    public bool displayable { get; set; }
    public int? purchasable_at { get; set; }
    public int streamable_at { get; set; }
    public bool hires { get; set; }
    public bool hires_streamable { get; set; }
    public string maximum_technical_specifications { get; set; }
}

public class Audio_Info
{
    public float replaygain_track_peak { get; set; }
    public float replaygain_track_gain { get; set; }
}

public class Performer
{
    public string name { get; set; }
    public int id { get; set; }
}

public class Album
{
    public Image1 image { get; set; }
    public int maximum_bit_depth { get; set; }
    public int media_count { get; set; }
    public Artist2 artist { get; set; }
    public string upc { get; set; }
    public int released_at { get; set; }
    public Label1 label { get; set; }
    public string title { get; set; }
    public int qobuz_id { get; set; }
    public string version { get; set; }
    public int duration { get; set; }
    public bool parental_warning { get; set; }
    public int tracks_count { get; set; }
    public Genre1 genre { get; set; }
    public int maximum_channel_count { get; set; }
    public string id { get; set; }
    public float maximum_sampling_rate { get; set; }
    public bool previewable { get; set; }
    public bool sampleable { get; set; }
    public bool displayable { get; set; }
    public bool streamable { get; set; }
    public int streamable_at { get; set; }
    public bool downloadable { get; set; }
    public object purchasable_at { get; set; }
    public bool purchasable { get; set; }
    public string release_date_original { get; set; }
    public string release_date_download { get; set; }
    public string release_date_stream { get; set; }
    public string release_date_purchase { get; set; }
    public bool hires { get; set; }
    public bool hires_streamable { get; set; }
    public string maximum_technical_specifications { get; set; }
}

public class Image1
{
    public string small { get; set; }
    public string thumbnail { get; set; }
    public string large { get; set; }
}

public class Artist2
{
    public object image { get; set; }
    public string name { get; set; }
    public int id { get; set; }
    public int albums_count { get; set; }
    public string slug { get; set; }
    public object picture { get; set; }
}

public class Label1
{
    public string name { get; set; }
    public int id { get; set; }
    public int albums_count { get; set; }
    public int supplier_id { get; set; }
    public string slug { get; set; }
}

public class Genre1
{
    public int[] path { get; set; }
    public string name { get; set; }
    public int id { get; set; }
    public string slug { get; set; }
}

public class Composer
{
    public string name { get; set; }
    public int id { get; set; }
}

public class Artists
{
    public int limit { get; set; }
    public int offset { get; set; }
    public int total { get; set; }
    public Item2[] items { get; set; }
}

public class Item2
{
    public string picture { get; set; }
    public Image2 image { get; set; }
    public string name { get; set; }
    public string slug { get; set; }
    public int albums_count { get; set; }
    public int id { get; set; }
}

public class Image2
{
    public string small { get; set; }
    public string medium { get; set; }
    public string large { get; set; }
    public string extralarge { get; set; }
    public string mega { get; set; }
}

public class Playlists
{
    public int limit { get; set; }
    public int offset { get; set; }
    public int total { get; set; }
    public Item3[] items { get; set; }
}

public class Item3
{
    public int id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public int tracks_count { get; set; }
    public int users_count { get; set; }
    public int duration { get; set; }
    public int public_at { get; set; }
    public int created_at { get; set; }
    public int updated_at { get; set; }
    public bool is_public { get; set; }
    public bool is_collaborative { get; set; }
    public Owner owner { get; set; }
    public int indexed_at { get; set; }
    public string slug { get; set; }
    public Genre2[] genres { get; set; }
    public string[] images { get; set; }
    public bool is_published { get; set; }
    public bool is_featured { get; set; }
    public int published_from { get; set; }
    public int published_to { get; set; }
    public string[] stores { get; set; }
    public Tag[] tags { get; set; }
    public string[] image_rectangle { get; set; }
    public string[] image_rectangle_mini { get; set; }
    public Featured_Artists[] featured_artists { get; set; }
    public int timestamp_position { get; set; }
    public string[] images150 { get; set; }
    public string[] images300 { get; set; }
}

public class Owner
{
    public int id { get; set; }
    public string name { get; set; }
}

public class Genre2
{
    public int id { get; set; }
    public string name { get; set; }
    public int[] path { get; set; }
    public string slug { get; set; }
}

public class Tag
{
    public string featured_tag_id { get; set; }
    public string name_json { get; set; }
    public string slug { get; set; }
    public Genre_Tag genre_tag { get; set; }
    public bool is_discover { get; set; }
}

public class Genre_Tag
{
    public string genre_id { get; set; }
    public string name { get; set; }
}

public class Featured_Artists
{
    public int id { get; set; }
    public string name { get; set; }
    public string slug { get; set; }
    public int albums_count { get; set; }
    public object picture { get; set; }
    public object image { get; set; }
}

public class Stories
{
    public int limit { get; set; }
    public int offset { get; set; }
    public int total { get; set; }
    public object[] items { get; set; }
}

public class Most_Popular
{
    public int limit { get; set; }
    public int offset { get; set; }
    public int total { get; set; }
    public Item4[] items { get; set; }
}

public class Item4
{
    public string type { get; set; }
    public Content content { get; set; }
}

public class Content
{
    public string type { get; set; }
    public int maximum_bit_depth { get; set; }
    public Image3 image { get; set; }
    public int media_count { get; set; }
    public Artist3 artist { get; set; }
    public Artist5[] artists { get; set; }
    public string upc { get; set; }
    public int released_at { get; set; }
    public Label2 label { get; set; }
    public string title { get; set; }
    public int qobuz_id { get; set; }
    public string version { get; set; }
    public string url { get; set; }
    public string slug { get; set; }
    public int duration { get; set; }
    public bool parental_warning { get; set; }
    public int popularity { get; set; }
    public int tracks_count { get; set; }
    public Genre3 genre { get; set; }
    public int maximum_channel_count { get; set; }
    public object id { get; set; }
    public float maximum_sampling_rate { get; set; }
    public Article[] articles { get; set; }
    public string release_date_original { get; set; }
    public string release_date_download { get; set; }
    public string release_date_stream { get; set; }
    public bool purchasable { get; set; }
    public bool streamable { get; set; }
    public bool previewable { get; set; }
    public bool sampleable { get; set; }
    public bool downloadable { get; set; }
    public bool displayable { get; set; }
    public int? purchasable_at { get; set; }
    public int streamable_at { get; set; }
    public bool hires { get; set; }
    public bool hires_streamable { get; set; }
    public string copyright { get; set; }
    public string performers { get; set; }
    public Audio_Info1 audio_info { get; set; }
    public Performer1 performer { get; set; }
    public Article_Ids article_ids { get; set; }
    public Album1 album { get; set; }
    public object work { get; set; }
    public Composer1 composer { get; set; }
    public string isrc { get; set; }
    public int track_number { get; set; }
    public int media_number { get; set; }
    public string release_date_purchase { get; set; }
    public string picture { get; set; }
    public string name { get; set; }
    public int albums_count { get; set; }
    public string maximum_technical_specifications { get; set; }
}

public class Image3
{
    public string small { get; set; }
    public string thumbnail { get; set; }
    public string large { get; set; }
    public object back { get; set; }
    public string medium { get; set; }
    public string extralarge { get; set; }
    public string mega { get; set; }
}

public class Artist3
{
    public object image { get; set; }
    public string name { get; set; }
    public int id { get; set; }
    public int albums_count { get; set; }
    public string slug { get; set; }
    public object picture { get; set; }
}

public class Label2
{
    public string name { get; set; }
    public int id { get; set; }
    public int albums_count { get; set; }
    public int supplier_id { get; set; }
    public string slug { get; set; }
}

public class Genre3
{
    public int[] path { get; set; }
    public string color { get; set; }
    public string name { get; set; }
    public int id { get; set; }
    public string slug { get; set; }
}

public class Audio_Info1
{
    public float replaygain_track_peak { get; set; }
    public float replaygain_track_gain { get; set; }
}

public class Performer1
{
    public string name { get; set; }
    public int id { get; set; }
}

public class Article_Ids
{
    public int LLS { get; set; }
    public int SMR { get; set; }
    public int SM2 { get; set; }
}

public class Album1
{
    public Image4 image { get; set; }
    public int maximum_bit_depth { get; set; }
    public int media_count { get; set; }
    public Artist4 artist { get; set; }
    public string upc { get; set; }
    public int released_at { get; set; }
    public Label3 label { get; set; }
    public string title { get; set; }
    public int qobuz_id { get; set; }
    public string version { get; set; }
    public int duration { get; set; }
    public bool parental_warning { get; set; }
    public int tracks_count { get; set; }
    public Genre4 genre { get; set; }
    public int maximum_channel_count { get; set; }
    public string id { get; set; }
    public float maximum_sampling_rate { get; set; }
    public bool previewable { get; set; }
    public bool sampleable { get; set; }
    public bool displayable { get; set; }
    public bool streamable { get; set; }
    public int streamable_at { get; set; }
    public bool downloadable { get; set; }
    public object purchasable_at { get; set; }
    public bool purchasable { get; set; }
    public string release_date_original { get; set; }
    public string release_date_download { get; set; }
    public string release_date_stream { get; set; }
    public string release_date_purchase { get; set; }
    public bool hires { get; set; }
    public bool hires_streamable { get; set; }
    public string maximum_technical_specifications { get; set; }
}

public class Image4
{
    public string small { get; set; }
    public string thumbnail { get; set; }
    public string large { get; set; }
}

public class Artist4
{
    public object image { get; set; }
    public string name { get; set; }
    public int id { get; set; }
    public int albums_count { get; set; }
    public string slug { get; set; }
    public object picture { get; set; }
}

public class Label3
{
    public string name { get; set; }
    public int id { get; set; }
    public int albums_count { get; set; }
    public int supplier_id { get; set; }
    public string slug { get; set; }
}

public class Genre4
{
    public int[] path { get; set; }
    public string name { get; set; }
    public int id { get; set; }
    public string slug { get; set; }
}

public class Composer1
{
    public string name { get; set; }
    public int id { get; set; }
}

public class Artist5
{
    public int id { get; set; }
    public string name { get; set; }
    public string[] roles { get; set; }
}

public class Article
{
    public int id { get; set; }
    public string url { get; set; }
    public float price { get; set; }
    public string currency { get; set; }
    public string type { get; set; }
    public string label { get; set; }
    public string description { get; set; }
}
