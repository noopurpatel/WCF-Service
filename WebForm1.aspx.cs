using Module7_WCF.DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel.Description;
using System.Text.Json;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Module7_WCF
{
	public partial class WebForm1 : System.Web.UI.Page
	{
		static List<Movie> movies = new List<Movie>();
		static string ConnectionString = ConfigurationManager.ConnectionStrings["MovieCS"].ConnectionString;
		protected void Page_Load(object sender, EventArgs e)
		{
			//ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "Calculate()", true);
			if (!Page.IsPostBack)
			{
				
				
			}
		}

		[WebMethod]
		public static string GetData()
		{
			movies.Clear();
			SqlConnection con = new SqlConnection(ConnectionString);
			con.Open();
			SqlCommand cmd = new SqlCommand("SELECT [MovieID]\r\n      ,[Title]\r\n      ,[Genre]\r\n      ,[ReleaseYear]\r\n      ,[Description]\r\n      ,[Duration]\r\n      ,[Rating]\r\n      ,[Director]\r\n      ,[IsAvailable]\r\n  FROM [Module6.DataAccess.MovieContext].[dbo].[Movies]", con);

			// Call ExecuteReader to return a DataReader
			SqlDataReader reader = cmd.ExecuteReader();

			DataTable dt = new DataTable();
			dt.Columns.AddRange(new DataColumn[9] 
			{ 
				    new DataColumn("Movie Id", typeof(int)),
					new DataColumn("Title", typeof(string)),
					new DataColumn("Genre",typeof(string)) ,
					new DataColumn("Release Year", typeof(string)) ,
					new DataColumn("Description",typeof(string)),
					new DataColumn("Duration",typeof(string)),
					new DataColumn("Movie Rating",typeof(string)),
					new DataColumn("Movie Director",typeof(string)),
					new DataColumn("Is Available",typeof(string)) });
				while (reader.Read())
			{
				Movie movie = new Movie();
				movie.MovieID = Int32.Parse(reader["MovieID"].ToString());
				movie.Title = reader["Title"].ToString();
				movie.Genre = Enum.GetName(typeof(Genre), Int32.Parse(reader["Genre"].ToString()));
				movie.ReleaseYear = Int32.Parse(reader["ReleaseYear"].ToString());
				movie.Description = reader["Description"].ToString();
				movie.Duration = Int32.Parse(reader["Duration"].ToString());
				movie.Rating = Double.Parse(reader["Rating"].ToString());
				movie.Director = reader["Director"].ToString();
				movie.IsAvailable = Boolean.Parse(reader["IsAvailable"].ToString());
				movies.Add(movie);
			}

			return JsonSerializer.Serialize(movies);


		}
	}
}