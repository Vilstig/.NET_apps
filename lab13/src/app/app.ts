import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router'; // RouterOutlet dla głównego routingu
import { FooterComponent } from './components/footer/footer';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterModule, FooterComponent, RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App {}