package Film.Interfaces;

import Film.Object.Film;

public interface Filmoteka {

	void  inicializeList (String sql);
	
	void deleteFilm (Film film);
	
	void editFilm(Film filmEdit, Film newFilm);
	
	void addFilm (Film film);
}
