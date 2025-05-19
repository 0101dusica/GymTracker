import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-simple-button',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss']
})
export class ButtonComponent {
  @Input() label: string = 'Submit';
  @Input() variant: 'primary' | 'outline' = 'primary';
  @Input() loading: boolean = false;
  @Input() disabled: boolean = false;

  @Output() submit = new EventEmitter<void>();

  onClick() {
    if (!this.loading && !this.disabled) {
      this.submit.emit();
    }
  }
}
