package Film.Controllers;

import java.io.File;
import java.io.IOException;

import Film.Interfaces.Impls.DB;
import Film.Object.Film;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.scene.Node;
import javafx.scene.control.Button;
import javafx.scene.control.ComboBox;
import javafx.scene.control.Label;
import javafx.scene.control.TextField;
import javafx.stage.FileChooser;
import javafx.stage.Stage;

public class EditController {
	@FXML
	private Button btnRes;
	@FXML
	private Button btnOk;
	@FXML
	private Button btnCancel;
	@FXML
	private Button btnImgPath;
	@FXML
	private Button btnFilmPath;
	@FXML
	private TextField txtNameR;
	@FXML
	private TextField txtNameE;
	@FXML
	private TextField txtYear;
	@FXML
	private TextField txtProducer;
	@FXML
	private TextField txtIMDB;
	@FXML
	private TextField txtKP;
	@FXML
	private TextField txtImgPath;
	@FXML
	private TextField txtFilmPath;
	@FXML
	private TextField txtDescription;
	@FXML
	private ComboBox cmbCountry;
	@FXML
	private ComboBox cmbGenre;
	@FXML
	private Label labelGenre;
	@FXML
	private Label labelError;
	DB dB = new DB();

	private ObservableList<String> countryList = FXCollections.observableArrayList("США", "Великобритания", "Германия",
			"Россия", "Франция");

	MainController mainController = new MainController();

	Stage mainStage = mainController.getStage();

	boolean error = true;
	
	private Film filmEdit;

	private Film film = new Film();

	private String genre[] = { "", "", "", "", "", "", "", "", "", "" };

	private int countGenre = 0;

	private String setItem = "";

	private boolean editGenre = false;
	
	private boolean editFilm = false;
	
	public void setEditFilm(boolean editFilm){
		this.editFilm = editFilm;
	}

	@FXML
	private void initialize() {
		cmbGenre.setItems(mainController.getGenreList());
		cmbCountry.setItems(countryList);
	}

	public void addFilm() {
		film.setNameR(txtNameR.getText());
		film.setNameE(txtNameE.getText());
		film.setName(film.getNameR() + " / " + film.getNameE());
		film.setYear(Integer.parseInt(txtYear.getText()));
		film.setRatingIMDB(txtIMDB.getText());
		film.setRatingKP(txtKP.getText());
		film.setProducer(txtProducer.getText());
		film.setImgPath(txtImgPath.getText());
		film.setVideoPath(txtFilmPath.getText());
		film.setDescription(txtDescription.getText());
	}

	public void editFilm(Film filmEdit) {
		this.filmEdit = filmEdit;
		cmbGenre.setValue(null);
		for (int i = 0; i < 10; i++)
			genre[i] = "";
		countGenre = 0;
		labelError.setText("");
		txtNameR.setText(filmEdit.getNameR());
		txtNameE.setText(filmEdit.getNameE());
		txtYear.setText(Integer.toString(filmEdit.getYear()));
		txtProducer.setText(filmEdit.getProducer());
		txtIMDB.setText(filmEdit.getRatingIMDB());
		txtKP.setText(filmEdit.getRatingKP());
		txtImgPath.setText(filmEdit.getImgPath());
		txtFilmPath.setText(filmEdit.getVideoPath());
		txtDescription.setText(filmEdit.getDescription());
		cmbCountry.setValue(filmEdit.getCountry());
		labelGenre.setText(filmEdit.getGenre());
		film.setGenre(filmEdit.getGenre());
		choiceCountry();
		if (editFilm)
		editGenre = true;
		else{ txtYear.setText(""); labelGenre.setText(""); editGenre = false;}
		error = true;
	}

	public void pressButton(ActionEvent actionEvent) throws IOException {
		Object source = actionEvent.getSource();

		if (!(source instanceof Button))
			return;

		Button clickedButton = (Button) source;

		switch (clickedButton.getId()) {

		case "btnOk":
			checkError();
			if (error)
				return;
			
			addFilm();
			setGenre();
			if (!editFilm)
			dB.addFilm(film);
			else
				dB.editFilm(filmEdit, film);
			dB.getFilmList().clear();
			dB.inicializeList("select * from ViewForMainTable");
			closeDialog(actionEvent);
			break;
		case "btnCancel":
			closeDialog(actionEvent);
			break;
		case "btnRes":
			editGenre = false;
			labelGenre.setText("");
			film.setGenre("");
			for (int i = 0; i < 10; i++)
				genre[i] = "";
			countGenre = 0;
			cmbGenre.setValue(null);
			break;
		case "btnImgPath":
			FileChooser fileChooserI = new FileChooser();
			FileChooser.ExtensionFilter extFilterI = new FileChooser.ExtensionFilter("Image Files", "*.jpg", "*.bmp",
					"*.png");
			fileChooserI.getExtensionFilters().add(extFilterI);
			File fileI = fileChooserI.showOpenDialog(mainStage);
			if (fileI != null) {
				txtImgPath.setText(fileI.getPath());
			}
			break;
		case "btnFilmPath":
			FileChooser fileChooserV = new FileChooser();
			FileChooser.ExtensionFilter extFilterV = new FileChooser.ExtensionFilter("Video Files", "*.mp4", "*.flv");
			fileChooserV.getExtensionFilters().add(extFilterV);
			File fileV = fileChooserV.showOpenDialog(mainStage);
			if (fileV != null) {
				txtFilmPath.setText(fileV.getPath());
			}
			break;

		}

	}

	private boolean checkError() {
		if (txtNameR.getText() == null || txtDescription.getText() == null || txtNameE.getText() == null){
			labelError.setText("Заполните все поля, помеченные \"*\" ");
			return error;
		}
		if (txtNameR.getText().length() == 0 || txtYear.getText().length() == 0 || txtIMDB.getText().length() == 0
				|| txtDescription.getText().length() == 0 || cmbCountry.getValue() == null || txtNameR.getText().length() == 0) {
			labelError.setText("Заполните все поля, помеченные \"*\" ");
			return error;
		}
		if (!editFilm){
			if (cmbGenre.getValue() == null){
				labelError.setText("Заполните все поля, помеченные \"*\" ");
			return error;}
	}

		try {
			Integer.parseInt(txtYear.getText());
		} catch (Exception e) {
			labelError.setText("Введите числовое значение в поле \"Год\"");
			return error;
		}
		txtIMDB.setText(txtIMDB.getText().replace(",", "."));
		try{
		txtKP.setText(txtKP.getText().replace(",", "."));
		} catch (Exception e) {}
		try {
			Double.parseDouble(txtIMDB.getText());
			Double.parseDouble(txtKP.getText());
		} catch (Exception e) {
			labelError.setText("Введите числовое значение в поля \"Рейтинг\"");
			return error;
		}
		if (Double.parseDouble(txtIMDB.getText()) > 10 || Double.parseDouble(txtKP.getText()) > 10) {
			labelError.setText("Рейтинг фильма не может быть больше 10");
			return error;
		}
		if (Double.parseDouble(txtIMDB.getText()) < 0 || Double.parseDouble(txtKP.getText()) < 0) {
			labelError.setText("Рейтинг фильма не может быть меньше 0");
			return error;
		}
		return error = false;
	}

	private void closeDialog(ActionEvent actionEvent) {
		Node source = (Node) actionEvent.getSource();
		Stage stage = (Stage) source.getScene().getWindow();
		stage.hide();
	}

	private void setGenre() {

		if (countGenre > 0) {
			if (!editGenre) {
				film.setGenre(genre[0]);
				for (int i = 1; i < countGenre; i++)
					film.setGenre(film.getGenre() + ", " + genre[i]);
			} else {
				for (int i = 0; i < countGenre; i++)
					film.setGenre(film.getGenre() + ", " + genre[i]);
			}
		}
	}

	@FXML
	private void choiceGenre() {
		if (cmbGenre.getValue() == null) {
			return;
		}

		setItem = (String) cmbGenre.getValue();
		for (int i = 0; i < 10; i++) {
			if (setItem == genre[i]) {
				return;
			}
		}
		if (setItem == "Жанр") {
			return;
		}

		switch (setItem) {
		case "комедия":
			if (labelGenre.getText().contains("комедия"))
				return;
			genre[countGenre] = "комедия";
			countGenre++;
			labelGenre.setText(labelGenre.getText() + " комедия");
			break;
		case "ужасы":
			if (labelGenre.getText().contains("ужасы"))
				return;
			genre[countGenre] = "ужасы";
			countGenre++;
			labelGenre.setText(labelGenre.getText() + " ужасы");
			break;
		case "триллер":
			if (labelGenre.getText().contains("триллер"))
				return;
			genre[countGenre] = "триллер";
			countGenre++;
			labelGenre.setText(labelGenre.getText() + " триллер");
			break;
		case "боевик":
			if (labelGenre.getText().contains("боевик"))
				return;
			genre[countGenre] = "боевик";
			countGenre++;
			labelGenre.setText(labelGenre.getText() + " боевик");
			break;
		case "мелодрама":
			if (labelGenre.getText().contains("мелодрама"))
				return;
			genre[countGenre] = "мелодрама";
			countGenre++;
			labelGenre.setText(labelGenre.getText() + " мелодрама");
			break;
		case "драма":
			if (labelGenre.getText().contains("драма"))
				return;
			genre[countGenre] = "драма";
			countGenre++;
			labelGenre.setText(labelGenre.getText() + " драма");
			break;
		case "мультфильм":
			if (labelGenre.getText().contains("мультфильм"))
				return;
			genre[countGenre] = "мультфильм";
			countGenre++;
			labelGenre.setText(labelGenre.getText() + " мультфильм");
			break;
		case "фантастика":
			if (labelGenre.getText().contains("фантастика"))
				return;
			genre[countGenre] = "фантастика";
			countGenre++;
			labelGenre.setText(labelGenre.getText() + " фантастика");
			break;
		case "фэнтези":
			if (labelGenre.getText().contains("фэнтези"))
				return;
			genre[countGenre] = "фэнтези";
			countGenre++;
			labelGenre.setText(labelGenre.getText() + " фэнтези");
			break;
		case "документальный":
			if (labelGenre.getText().contains("документальный"))
				return;
			genre[countGenre] = "документальный";
			countGenre++;
			labelGenre.setText(labelGenre.getText() + " документальный");
			break;
		}

	}

	@FXML
	private void choiceCountry() {
		if (cmbCountry.getValue() == null)
			return;
		switch ((String) cmbCountry.getValue()) {
		case "США":
			film.setCountry("1");
			break;
		case "Великобритания":
			film.setCountry("2");
			break;
		case "Германия":
			film.setCountry("3");
			break;
		case "Россия":
			film.setCountry("4");
			break;
		case "Франция":
			film.setCountry("5");
			break;
		}
	}
	private void setCountry(){
		if (cmbCountry.getValue() == null)
			return;
		switch ((String) cmbCountry.getValue()) {
		case "США":
			film.setCountry("1");
			break;
		case "Великобритания":
			film.setCountry("2");
			break;
		case "Германия":
			film.setCountry("3");
			break;
		case "Россия":
			film.setCountry("4");
			break;
		case "Франция":
			film.setCountry("5");
			break;
		}
	}
}
