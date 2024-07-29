using Module7_WCF.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text.Json;

namespace Module7_WCF
{
	[ServiceContract(Namespace = "")]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class MovieService
	{
		string ConnectionString = ConfigurationManager.ConnectionStrings["MovieCS"].ConnectionString;
		[OperationContract]
		public string GetAllMovies()
		{
			List<Movie> movies = new List<Movie>();
			movies.Clear();
			SqlConnection con = new SqlConnection(ConnectionString);
			SqlConnection con2 = new SqlConnection(ConnectionString);
			con.Open();
			SqlCommand cmd = new SqlCommand("SELECT [MovieID]\r\n      ,[Title]\r\n      ,[Genre]\r\n      ,[ReleaseYear]\r\n      ,[Description]\r\n      ,[Duration]\r\n      ,[Rating]\r\n      ,[Director]\r\n      ,[IsAvailable]\r\n  FROM [Module6.DataAccess.MovieContext].[dbo].[Movies]", con);

			// Call ExecuteReader to return a DataReader
			SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				SqlCommand cmdCast = new SqlCommand($"SELECT [CastID], [Name], [MovieId]  FROM [Module6.DataAccess.MovieContext].[dbo].[Casts] Where MovieId = {Int32.Parse(reader["MovieID"].ToString())}", con2);

				

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
				movie.Cast = new List<Cast>();
				
				con2.Open();
				SqlDataReader readerCast = cmdCast.ExecuteReader();
				while (readerCast.Read())
				{
					Cast cast = new Cast();
					movie.Cast.Add(new Cast { CastId = Int32.Parse(readerCast["CastId"].ToString()), Name = readerCast["Name"].ToString(), MovieId= Int32.Parse(readerCast["MovieID"].ToString()) });
				}

					movies.Add(movie);
				con2.Close();
			}

			con.Close(); 

			return JsonSerializer.Serialize(movies);
		}

		[OperationContract]
		public string GetMovieDetail(int movieId)
		{
			Movie movie = new Movie();
			SqlConnection con = new SqlConnection(ConnectionString);
			SqlConnection con2 = new SqlConnection(ConnectionString);
			con.Open();
			SqlCommand cmd = new SqlCommand($"SELECT [MovieID]\r\n      ,[Title]\r\n      ,[Genre]\r\n      ,[ReleaseYear]\r\n      ,[Description]\r\n      ,[Duration]\r\n      ,[Rating]\r\n      ,[Director]\r\n      ,[IsAvailable]\r\n  FROM [Module6.DataAccess.MovieContext].[dbo].[Movies] Where MovieID = {movieId}", con);

			// Call ExecuteReader to return a DataReader
			SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				SqlCommand cmdCast = new SqlCommand($"SELECT [CastID], [Name], [MovieId]  FROM [Module6.DataAccess.MovieContext].[dbo].[Casts] Where MovieId = {Int32.Parse(reader["MovieID"].ToString())}", con2);



				movie.MovieID = Int32.Parse(reader["MovieID"].ToString());
				movie.Title = reader["Title"].ToString();
				movie.Genre = Enum.GetName(typeof(Genre), Int32.Parse(reader["Genre"].ToString()));
				movie.ReleaseYear = Int32.Parse(reader["ReleaseYear"].ToString());
				movie.Description = reader["Description"].ToString();
				movie.Duration = Int32.Parse(reader["Duration"].ToString());
				movie.Rating = Double.Parse(reader["Rating"].ToString());
				movie.Director = reader["Director"].ToString();
				movie.IsAvailable = Boolean.Parse(reader["IsAvailable"].ToString());
				movie.Cast = new List<Cast>();

				con2.Open();
				SqlDataReader readerCast = cmdCast.ExecuteReader();
				while (readerCast.Read())
				{
					Cast cast = new Cast();
					movie.Cast.Add(new Cast { CastId = Int32.Parse(readerCast["CastId"].ToString()), Name = readerCast["Name"].ToString(), MovieId = Int32.Parse(readerCast["MovieID"].ToString()) });
				}

				con2.Close();
			}

			con.Close();

			return JsonSerializer.Serialize(movie);
		}

		//public void AddMovie(string title, string genre, int releaseYear, string description, int duration, int rating, string director, bool isAvailable)
		//{
		//	using (SqlConnection con = new SqlConnection(ConnectionString))
		//	{
		//		using (SqlCommand cmd = new SqlCommand("INSERT INTO Customers (Name, Country) VALUES (@Name, @Country)"))
		//		{
		//			cmd.Parameters.AddWithValue("@Name", name);
		//			cmd.Parameters.AddWithValue("@Country", country);
		//			cmd.Connection = con;
		//			con.Open();
		//			cmd.ExecuteNonQuery();
		//			con.Close();
		//		}
		//	}
		//}
	}
}
