import { Component, inject, OnInit, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-root', // nazwa tagu w HTML: <app-root>
  imports: [], // komponenty/moduły używane w template
  templateUrl: './app.html', // plik HTML dla tego komponentu
  styleUrl: './app.css' // style CSS
})
export class App implements OnInit {

  // Wstrzykuje HttpClient do wysyłania requestów HTTP
  private http = inject(HttpClient);

  // Signal - reaktywna zmienna śledzona przez Angular
  protected readonly title = signal('Dating app');

  // Signal przechowujący listę członków z API
  protected members = signal<any>([]);

  // Wywołuje się automatycznie przy inicjalizacji komponentu
  ngOnInit(): void {
    // Wysyła GET request do API
    this.http.get("https://localhost:5001/api/members").subscribe({
      next: response => this.members.set(response), // przy sukcesie: zapisz dane
      error: error => console.log(error), // przy błędzie: wyświetl w konsoli
      complete: () => console.log("Request completed") // gdy zakończone
    })
  }
}
