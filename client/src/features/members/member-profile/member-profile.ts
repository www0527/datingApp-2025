import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map, Observable, single } from 'rxjs';
import { Member } from '../../../type/Member';
import { AsyncPipe, DatePipe } from '@angular/common';

@Component({
    selector: 'app-member-profile',
    imports: [AsyncPipe, DatePipe],
    templateUrl: './member-profile.html',
    styleUrl: './member-profile.css',
})
export class MemberProfile implements OnInit {
    private route = inject(ActivatedRoute);
    protected member$!: Observable<Member | undefined>;

    ngOnInit(): void {
        this.member$ = this.route.parent?.data.pipe(
            map(data => data['member']),
        ) as Observable<Member | undefined>;
    }
}
