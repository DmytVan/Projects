package Film.Interfaces.Impls;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

import Film.Object.Film;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;


public class DB {

	static Connection con = null;
	static Statement stmt = null;
	static ResultSet res = null;
	private static ObservableList<Film> filmList = FXCollections.observableArrayList();
	private String descriptionId = "";
	private String nameId = "";
	
	 public void add(Film film) {
	        filmList.add(film);}

	public  ObservableList<Film> getFilmList() {
		return filmList;
	}
	
	

	public static void DBConnect() throws ClassNotFoundException {

		Class.forName("org.sqlite.JDBC");

		try {

			String url = "jdbc:sqlite:VIDEO.db";
			con = DriverManager.getConnection(url);


		} catch (SQLException e) {
			e.printStackTrace();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public static void DBClose() {
		try {
			if (res != null)
				res.close();
			if (stmt != null)
				((Connection) stmt).close();
			if (con != null)
				con.close();
		} catch (Exception ex) {

		}
	}
	public  void inicializeList(String sql){
		try {
		DBConnect();
		
		 stmt = (Statement) con.createStatement();
		 res = ((java.sql.Statement) stmt).executeQuery(sql);
		
		 while (res.next()) {
		 filmList.add(new Film(res.getString("name_ru") + " / " + res.getString("name_en"), res.getInt("year"),
				 res.getString("country"), res.getString("genre"), "IMDB: " + res.getString("rating_IMDB") + " KP: " +  res.getString("rating_kp"),
				 res.getString("description"), res.getString("producer"), res.getString("imgPath"), res.getString("videoPath"),
				 res.getString("name_ru"), res.getString("rating_IMDB"), res.getString("rating_kp"), res.getString("name_en"),
				 res.getString("id"), res.getString("name_id"), res.getString("rating_id"), res.getString("description_id")));
		 }
		 } catch (SQLException e) {
				e.printStackTrace();
			} catch (Exception e) {
				e.printStackTrace();
			}finally{
		DBClose();
			}
		 }
	
	public void addFilm(Film film){
		try {
			DBConnect();
			
			 stmt = (Statement) con.createStatement();
			 stmt.executeUpdate(
					 "insert into spr_name (name_ru, name_en) values (\""+ film.getNameR().replace("\"", "'") + "\", \"" + film.getNameE().replace("\"", "'") + "\")");
			 stmt.executeUpdate(
					 "insert into spr_evaluation (rating_kp, rating_IMDB) values (\"" + film.getRatingKP() + "\", \"" + film.getRatingIMDB() + "\")");
			 stmt.executeUpdate(
					 "insert into spr_description (description) values (\"" + film.getDescription().replace("\"", "'") + "\")");
			 stmt.executeUpdate(
					 "insert into video (year, producer, genre, country, imgPath, videoPath) values (\"" + film.getYear() + "\", \""
			 + film.getProducer().replace("\"", "'") + "\", \"" + film.getGenre() + "\", \"" + film.getCountry() + "\", \"" + film.getImgPath() + "\", \"" + film.getVideoPath() + "\")");
			 res = ((java.sql.Statement) stmt).executeQuery("select spr_name.[id] as d from spr_name where id=(select Max(`id`) FROM   `spr_name`)");
			 nameId = res.getString("d");
			 res = ((java.sql.Statement) stmt).executeQuery("select spr_description.[id] as dd from spr_description where id=(select Max(`id`) FROM   `spr_description`)");
			 descriptionId = res.getString("dd");
			 res = ((java.sql.Statement) stmt).executeQuery("select spr_evaluation.[id] as ddd from spr_evaluation where id=(select Max(`id`) FROM   `spr_evaluation`)");
			 stmt.executeUpdate(
					 "update video set name = " + nameId + ", rating = " + res.getString("ddd") + ", description = " + descriptionId
					  + " where id=(select Max(`id`) FROM   `video`)");

			 } catch (SQLException e) {
					e.printStackTrace();
				} catch (Exception e) {
					e.printStackTrace();
				}finally{
			DBClose();
				}
	}
	
	public void deleteFilm(Film film){
		if (film == null)
			return;
		try {
			DBConnect();
			
			stmt = (Statement) con.createStatement();
			
			stmt.executeUpdate("delete from video where id =" + film.getFilmId());
			 stmt.executeUpdate("delete from spr_name where id = " + film.getNameId());
			 stmt.executeUpdate("delete from spr_evaluation where id = " + film.getRatingId());
			 stmt.executeUpdate("delete from spr_description where id = " + film.getDescriptionId());
			 } catch (SQLException e) {
					e.printStackTrace();
				} catch (Exception e) {
					e.printStackTrace();
				}finally{
			DBClose();
				}
	}
	
	public void editFilm(Film filmEdit, Film newFilm){
		try {
			DBConnect();
			
			stmt = (Statement) con.createStatement();
			
			stmt.executeUpdate("update spr_name set name_ru = \"" + newFilm.getNameR().replace("\"", "'") + "\", name_en = \"" + newFilm.getNameE().replace("\"", "'") + "\" where id = " + filmEdit.getNameId());
			 stmt.executeUpdate("update spr_evaluation set rating_kp = \"" + newFilm.getRatingKP() + "\", rating_IMDB = \"" + newFilm.getRatingIMDB() + "\" where id = " + filmEdit.getRatingId());
			 stmt.executeUpdate("update spr_description set description = \"" + newFilm.getDescription().replace("\"", "'") + "\" where id = " + filmEdit.getDescriptionId());
			 stmt.executeUpdate("update video set year = \"" +  newFilm.getYear() + "\", producer = \"" + newFilm.getProducer().replace("\"", "'") + "\", genre = \"" + newFilm.getGenre()
			 + "\", country = \"" + newFilm.getCountry() + "\", imgPath = \"" + newFilm.getImgPath() + "\", videoPath = \"" + newFilm.getVideoPath()
			 + "\" where id = " + filmEdit.getFilmId());
			 } catch (SQLException e) {
					e.printStackTrace();
				} catch (Exception e) {
					e.printStackTrace();
				}finally{
			DBClose();
				}		
	}
	
    public static void print(){
        int number = 0;
        System.out.println();
        for(Film film: filmList){
            number++;
            
            System.out.println("\n\n"+number+") name = "+film.getName()+
            		"; year = "+film.getYear() + "; genre = " + film.getGenre() + "; rating = " + film.getRating() + "  " + film.getVideoPath());
            String test = film.getVideoPath();
        }
    }
		 
	}
	

