package Film.Start;
	
import java.io.IOException;

import Film.Interfaces.Impls.DB;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.stage.Stage;
import Film.Controllers.MainController;
import javafx.scene.Parent;
import javafx.scene.Scene;


public class Main extends Application {
	@Override
	public void start(Stage primaryStage) throws IOException {
		
		FXMLLoader fxmlLoader = new FXMLLoader();
        fxmlLoader.setLocation(getClass().getResource("../fxml/main.fxml"));
        Parent fxmlMain = fxmlLoader.load();
        MainController mainController = fxmlLoader.getController();
        mainController.setMainStage(primaryStage);
    	
    	
    	primaryStage.setTitle("Filmoteka");
        primaryStage.setMinHeight(350);
        primaryStage.setMinWidth(250);
        Scene scene = new Scene(fxmlMain, 920, 400);
        scene.getStylesheets().add(0, "Film/styles/main.css");
        primaryStage.setScene(scene);
        primaryStage.show();
    	
        
        
	}
	
	
	public static void main(String[] args) {
		launch(args);
	}
}