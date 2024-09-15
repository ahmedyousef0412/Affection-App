import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { PaginatedList } from '../../core/models/pagination';
import { Member } from '../../core/models/members';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrl: './pagination.component.css'
})
export class PaginationComponent implements OnChanges {


  @Input() pagination: PaginatedList<Member>;
  @Output() pageChanged = new EventEmitter<number>();

  pages: number[] = [];

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['pagination'] && this.pagination) {
      this.pages = this.createPageArray();
    }
  }

  createPageArray(): number[] {
    const pages = [];
    for (let i = 1; i <= this.pagination.totalPages; i++) {
      pages.push(i);
    }
    return pages;
  }

  goToPage(page: number): void {
    if (page !== this.pagination.pageNumber) {
      this.pageChanged.emit(page);
    }
  }

  previousPage(): void {
    if (this.pagination.hasPreviousPage) {
      this.pageChanged.emit(this.pagination.pageNumber - 1);
    }
  }

  nextPage(): void {
    if (this.pagination.hasNextPage) {
      this.pageChanged.emit(this.pagination.pageNumber + 1);
    }
  }
}
