package Film.Controllers;

import java.io.IOException;

import Film.Interfaces.Impls.DB;
import Film.Object.Film;
import javafx.collections.FXCollections;
import javafx.collections.ListChangeListener;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.input.MouseEvent;
import javafx.stage.Modality;
import javafx.stage.Stage;
import javafx.stage.WindowEvent;

public class MainController {

	@FXML
	private Button btnRes;
	@FXML
	private Label labelCount;
	@FXML
	private Label labelGenre;
	@FXML
	private TableView tblMain;
	@FXML
	private TableColumn<Film, String> columnName;
	@FXML
	private TableColumn<Film, String> columnYear;
	@FXML
	private TableColumn<Film, String> columnGenre;
	@FXML
	private TableColumn<Film, String> columnRating;
	@FXML
	private TextField txtSearch;
	@FXML
	private ComboBox cmbYear;
	@FXML
	private ComboBox cmbGenre;
	@FXML
	private Button btnAdd;
	@FXML
	private Button btnDell;
	@FXML
	private Button btnEdit;
	
	private Parent fxmlEdit;
	
	private Parent fxmlEditFilm;
	
	DB dB = new DB();

	private FXMLLoader fxmlLoader = new FXMLLoader();
	
	private FXMLLoader fxmlEditLoader = new FXMLLoader();

	private FilmController filmDialog;
	
	private EditController editController;

	private Stage filmDialogStage;
	
	private Stage editStage;

	private Stage mainStage;

	private ObservableList<Film> backupList;
	
	private ObservableList<Film> searchGenreList;
	
	private String genre[] = {"","","","","","","","","",""};
	
	private int countGenre=0;
	
	private String setItem = "";
	
	
	

	FilmController filmController = new FilmController();

	public void setMainStage(Stage mainStage) {
		this.mainStage = mainStage;
	}

	public Stage getStage() {
		return filmDialogStage;
	}
	ObservableList<String> yearList = 
			FXCollections.observableArrayList("Все", "...", "2011", "2012","2013", "2014", "2015", "2016", "2015>", "2010-2015", "2005-2009", "<2004");
	ObservableList<String> genreList = 
			FXCollections.observableArrayList("комедия", "ужасы", "триллер", "боевик","мелодрама", "драма",
					"мультфильм", "фантастика", "фэнтези", "документальный");
	
	public ObservableList getGenreList(){
		return genreList;		
	}

	@FXML
	private void initialize() {

		columnName.setCellValueFactory(new PropertyValueFactory<Film, String>("name"));
		columnYear.setCellValueFactory(new PropertyValueFactory<Film, String>("year"));
		columnGenre.setCellValueFactory(new PropertyValueFactory<Film, String>("genre"));
		columnRating.setCellValueFactory(new PropertyValueFactory<Film, String>("rating"));
		initListeners();
		dB.inicializeList("select * from ViewForMainTable");
		fillData();
		initLoader();
		loadEditDialog();

	}

	private void fillData() {
		if (backupList != null)
		backupList.clear();
		backupList = FXCollections.observableArrayList();
		backupList.addAll(dB.getFilmList());
		tblMain.setItems(dB.getFilmList());
		cmbYear.setItems(yearList);
		cmbGenre.setItems(genreList);
		
	}
	
	@FXML
	private void choiceYear(){
		if(cmbYear.getValue() == "Год"){
			return;}
		setItem = (String) cmbYear.getValue();
		if (setItem.equals("Все") || setItem.equals("...") || setItem.equals("2015>") || setItem.equals("2010-2015")
				|| setItem.equals("2005-2009") || setItem.equals("<2004")){
		switch (setItem){
		case "Все":
			dB.getFilmList().clear();
			dB.inicializeList("select * from ViewForMainTable");
			break;
		case "...":
			dB.getFilmList().clear();
			dB.inicializeList("select * from ViewForMainTable where year<2011");
		break;
		case "2015>":
			dB.getFilmList().clear();
			dB.inicializeList("select * from ViewForMainTable where year>2015");
		break;
		case "2010-2015":
			dB.getFilmList().clear();
			dB.inicializeList("select * from ViewForMainTable where year<2016 and year>2009");
		break;
		case "2005-2009":
			dB.getFilmList().clear();
			dB.inicializeList("select * from ViewForMainTable where year<2010 and year>2004");
		break;
		case "<2004":
			dB.getFilmList().clear();
			dB.inicializeList("select * from ViewForMainTable where year<2005");
		break;}
		}else{
			dB.getFilmList().clear();
			dB.inicializeList("select * from ViewForMainTable where year=" + setItem);}
		if (countGenre>0){	
			genreSearch();}
		fillData();
		searchName();
		
	}
	
	@FXML
	private void choiceGenre(){
		if(cmbGenre.getValue() == null){
			return;}
		
		setItem = (String) cmbGenre.getValue();
		for(int i=0 ; i<10 ;i++){
			if(setItem == genre[i]){
				return;}
		}
		if(setItem == "Жанр"){
			return;}
		
			switch (setItem){
			case "комедия":
				genre[countGenre] = "комедия";
				countGenre++;
				labelGenre.setText(labelGenre.getText() + " комедия");
				break;
			case "ужасы":
				genre[countGenre] = "ужасы";
				countGenre++;
				labelGenre.setText(labelGenre.getText() + " ужасы");
				break;
			case "триллер":
				genre[countGenre] = "триллер";
				countGenre++;
				labelGenre.setText(labelGenre.getText() + " триллер");
				break;
			case "боевик":
				genre[countGenre] = "боевик";
				countGenre++;
				labelGenre.setText(labelGenre.getText() + " боевик");
				break;
			case "мелодрама":
				genre[countGenre] = "мелодрама";
				countGenre++;
				labelGenre.setText(labelGenre.getText() + " мелодрама");
				break;
			case "драма":
				genre[countGenre] = "драма";
				countGenre++;
				labelGenre.setText(labelGenre.getText() + " драма");
				break;
			case "мультфильм":
				genre[countGenre] = "мультфильм";
				countGenre++;
				labelGenre.setText(labelGenre.getText() + " мультфильм");
				break;
			case "фантастика":
				genre[countGenre] = "фантастика";
				countGenre++;
				labelGenre.setText(labelGenre.getText() + " фантастика");
				break;
			case "фэнтези":
				genre[countGenre] = "фэнтези";
				countGenre++;
				labelGenre.setText(labelGenre.getText() + " фэнтези");
				break;
			case "документальный":
				genre[countGenre] = "документальный";
				countGenre++;
				labelGenre.setText(labelGenre.getText() + " документальный");
				break;
			}
			if (countGenre>0){	
				genreSearch();
			}
		}
	
	public void genreSearch(){
		searchGenreList = FXCollections.observableArrayList();
		searchGenreList.addAll(dB.getFilmList());
		dB.getFilmList().clear();
		
			for (Film film : searchGenreList) {
				if (film.getGenre().toLowerCase().contains(genre[0]) &&
					film.getGenre().toLowerCase().contains(genre[1]) &&
					film.getGenre().toLowerCase().contains(genre[2]) &&
					film.getGenre().toLowerCase().contains(genre[3]) &&
					film.getGenre().toLowerCase().contains(genre[4]) &&
					film.getGenre().toLowerCase().contains(genre[5]) &&
					film.getGenre().toLowerCase().contains(genre[6]) &&
					film.getGenre().toLowerCase().contains(genre[7]) &&
					film.getGenre().toLowerCase().contains(genre[8]) &&
					film.getGenre().toLowerCase().contains(genre[9]))
					dB.getFilmList().add(film);}
	}

	private void initListeners() {
		dB.getFilmList().addListener(new ListChangeListener<Film>() {
			@Override
			public void onChanged(Change<? extends Film> c) {
				updateCountLabel();
			}
		});

		tblMain.setOnMouseClicked(new EventHandler<MouseEvent>() {
			@Override
			public void handle(MouseEvent event) {
				if (event.getClickCount() == 2) {

					filmDialog.setFilm((Film) tblMain.getSelectionModel().getSelectedItem());
					showDialog();

				}
			}
		});
		

	}

	public void pressButton(ActionEvent actionEvent) throws IOException {
		Object source = actionEvent.getSource();

		if (!(source instanceof Button))
			return;

		Button clickedButton = (Button) source;

		switch (clickedButton.getId()) {
		case "btnRes":
			labelGenre.setText("");
			for (int i = 0; i<10; i++)
				genre[i] = "";
			cmbYear.setValue("Год");
			cmbGenre.setValue("Жанр");
			dB.getFilmList().clear();
			dB.inicializeList("select * from ViewForMainTable");
			txtSearch.setText("");
			fillData();
			countGenre = 0;
			break;
		case "btnAdd":
			editController.setEditFilm(false);
			editController.editFilm(new Film());
			showEditDialog();
			break;
		case "btnDell":
			dB.deleteFilm((Film) tblMain.getSelectionModel().getSelectedItem());
			dB.getFilmList().clear();
			dB.inicializeList("select * from ViewForMainTable");
			break;
		case "btnEdit":
			if ((Film) tblMain.getSelectionModel().getSelectedItem() == null)
				return;
			editController.setEditFilm(true);
			editController.editFilm((Film) tblMain.getSelectionModel().getSelectedItem());
			showEditDialog();
		}

	}

	private void updateCountLabel() {
		labelCount.setText("Количество записей: " + dB.getFilmList().size());
	}

	private void initLoader() {

		try {
			fxmlLoader.setLocation(getClass().getResource("../FXML/film.fxml"));
			fxmlEdit = fxmlLoader.load();
			filmDialog = fxmlLoader.getController();

		} catch (IOException e) {
			e.printStackTrace();
		}

	}
	
	private void loadEditDialog(){
		try{
			fxmlEditLoader.setLocation(getClass().getResource("../fxml/edit.fxml"));
			fxmlEditFilm = fxmlEditLoader.load();
			editController = fxmlEditLoader.getController();
		} catch (IOException e) {
			e.printStackTrace();
		}

	}
	
	private void showEditDialog(){
        
        if (editStage == null) {
        	editStage = new Stage();
        	editStage.setTitle("Film");
        	editStage.setMinHeight(150);
        	editStage.setMinWidth(300);
        	editStage.setResizable(false);
        	Scene scene = new Scene(fxmlEditFilm);
    		editStage.setScene(scene);
			editStage.initModality(Modality.WINDOW_MODAL);
			scene.getStylesheets().add(0, "Film/styles/edit.css");
			editStage.initOwner(mainStage);}
       editStage.showAndWait();
	}
	
	public void showDialog() {
		if (filmDialogStage == null) {
			filmDialogStage = new Stage();
			filmDialogStage.setTitle("Film");
			filmDialogStage.setMinHeight(150);
			filmDialogStage.setMinWidth(300);
			filmDialogStage.setResizable(false);
			Scene scene = new Scene(fxmlEdit);
			filmDialogStage.setScene(scene);
			filmDialogStage.initModality(Modality.WINDOW_MODAL);
			scene.getStylesheets().add(0, "Film/styles/film.css");
			filmDialogStage.initOwner(mainStage);
			if(tblMain.getSelectionModel().getSelectedItem() != null){
			filmDialogStage.setOnCloseRequest(new EventHandler<WindowEvent>() {

				public void handle(WindowEvent event) {

					FilmController.stopVideo();
			}
			});
		}
			

		}
		filmDialogStage.showAndWait();
		
	}
	
	public void filmSearch(ActionEvent actionEvent) {
		searchName();
		
		genreSearch();
	}
	
	private void searchName(){
		dB.getFilmList().clear();

		for (Film film : backupList) {
			if (film.getName().toLowerCase().contains(txtSearch.getText().toLowerCase())
					|| film.getGenre().toLowerCase().contains(txtSearch.getText().toLowerCase()))
				dB.getFilmList().add(film);
		}
	}
}
