package Film.Object;

public class Film {
	private String name;
	private int year;
	private String country;
	private String genre;
	private String rating;
	private String description;
	private String imgPath;
	private String videoPath;
	private String producer;
	private String nameR;
	private String nameE;
	private String ratingIMDB;
	private String ratingKP;
	private String filmId;
	private String nameId;
	private String ratingId;
	private String descriptionId;
	
	
	public Film(String name, int year, String country, String genre, String rating, String description, String producer,
			String imgPath, String videoPath, String nameR, String ratingIMDB,String ratingKP, String nameE, String filmId,
			String nameId, String ratingId, String descriptionId) {
		this.name = name;
		this.year = year;
		this.country = country;
		this.genre = genre;
		this.rating = rating;
		this.description = description;
		this.producer = producer;
		this.imgPath = imgPath;
		this.videoPath  = videoPath;
		this.nameR = nameR;
		this.ratingIMDB = ratingIMDB;
		this.ratingKP = ratingKP;
		this.nameE = nameE;
		this.filmId = filmId;
		this.nameId = nameId;
		this.ratingId = ratingId;
		this.descriptionId = descriptionId;
	}
	public Film() {
	}
	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	public int getYear() {
		return year;
	}
	public void setYear(int year) {
		this.year = year;
	}
	public String getGenre() {
		return genre;
	}
	public void setGenre(String genre) {
		this.genre = genre;
	}
	public String getRating() {
		return rating;
	}
	public void setScore(String score) {
		this.rating = score;
	}
	public String getDescription() {
		return description;
	}
	public void setDescription(String comments) {
		this.description = comments;
	}
	public String getImgPath() {
		return imgPath;
	}
	public void setImgPath(String imgPath) {
		this.imgPath = imgPath;
	}
	public String getVideoPath() {
		return videoPath;
	}
	public void setVideoPath(String videoPath) {
		this.videoPath = videoPath;
	}
	public String getProducer() {
		return producer;
	}
	public void setProducer(String producer) {
		this.producer = producer;
	}
	public String getCountry() {
		return country;
	}
	public void setCountry(String country) {
		this.country = country;
	}
	public String getNameR() {
		return nameR;
	}
	public void setNameR(String nameR) {
		this.nameR = nameR;
	}
	public String getNameE() {
		return nameE;
	}
	public void setNameE(String nameE) {
		this.nameE = nameE;
	}
	public String getRatingIMDB() {
		return ratingIMDB;
	}
	public void setRatingIMDB(String ratingIMDB) {
		this.ratingIMDB = ratingIMDB;
	}
	public String getRatingKP() {
		return ratingKP;
	}
	public void setRatingKP(String ratingKP) {
		this.ratingKP = ratingKP;
	}
	public String getFilmId() {
		return filmId;
	}
	public void setFilmId(String filmId) {
		this.filmId = filmId;
	}
	public String getNameId() {
		return nameId;
	}
	public void setNameId(String nameId) {
		this.nameId = nameId;
	}
	public String getRatingId() {
		return ratingId;
	}
	public void setRatingId(String ratingId) {
		this.ratingId = ratingId;
	}
	public String getDescriptionId() {
		return descriptionId;
	}
	public void setDescriptionId(String descriptionId) {
		this.descriptionId = descriptionId;
	}
	
	
	

	
	
}
/*
*/