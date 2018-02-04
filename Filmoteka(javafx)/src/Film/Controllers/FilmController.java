package Film.Controllers;

import java.io.File;

import Film.Object.Film;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.layout.Pane;
import javafx.scene.media.Media;
import javafx.scene.media.MediaPlayer;
import javafx.scene.media.MediaView;

public class FilmController {
	
	
	@FXML
	private Button btnPlay;
	@FXML
	private Button btnPause;
	@FXML
	private Button btnStop;
	@FXML
	private Label labelName;
	@FXML
	private Label labelCountry;
	@FXML
	private Label labelYear;
	@FXML
	private Label labelProducer;
	@FXML
	private Label labelGenre;
	@FXML
	private Label labelRating;
	@FXML
	private Label labelDescription;
	@FXML
	private MediaView media;
	@FXML
	private ImageView img;
	@FXML
	private Pane videoPane;
	
	private Film film;
	private static MediaPlayer mp;
	private Media me;
	
	private String videoPath;
	


	
	public void setFilm(Film film){
		if (film == null){
			return;}
		
		this.film = film;
		labelName.setText(film.getName());
		labelCountry.setText("Страна: " + film.getCountry());
		labelYear.setText("Год: " + film.getYear());
		labelProducer.setText("Продюссер: " + film.getProducer());
		labelGenre.setText("Жанр: " + film.getGenre());
		labelRating.setText("Рейтинг: " + film.getRating());
		labelDescription.setText("Описание: " + film.getDescription());
		try{
			if (!film.getImgPath().contains("\\")){
		Image im = new Image(this.getClass().getResource(film.getImgPath()).toString());
		img.setImage(im);}
			else{
		Image im = new Image("file:///" + film.getImgPath().replace("\\", "/"));
		img.setImage(im);}
		}
		catch(Exception e){
			Image im = new Image(this.getClass().getResource("Resources/Img/index.jpg").toString());

			img.setImage(im);
		}
		if (film.getImgPath().length() == 0){
			Image im = new Image(this.getClass().getResource("Resources/Img/index.jpg").toString());
		img.setImage(im);}
		if (!film.getVideoPath().contains("\\"))
		videoPath = film.getVideoPath();
		else
			videoPath = film.getVideoPath().replace("\\", "/");
		try{
		videoPath = new File(film.getVideoPath()).getAbsolutePath();
		me = new Media(new File(videoPath).toURI().toString());
		}catch(Exception e){	
			videoPath = new File("src/Film/Video/Error.mp4").getAbsolutePath();
			me = new Media(new File(videoPath).toURI().toString());
		}
		mp = new MediaPlayer(me);
		media.setMediaPlayer(mp);
		
	}
	
	 public void pressButton(ActionEvent actionEvent) {
		 if (film == null){
				return;}
		 Object source = actionEvent.getSource();

			if (!(source instanceof Button))
				return;

			Button clickedButton = (Button) source;
			
			switch (clickedButton.getId()) {
			case "btnPlay":
				mp.play();
				break;
			case "btnPause":
				mp.pause();
				break;
			case "btnStop":
				mp.stop();
				break;
				
			}
	 }
	
	public static void stopVideo(){
		mp.stop();
	}

}
