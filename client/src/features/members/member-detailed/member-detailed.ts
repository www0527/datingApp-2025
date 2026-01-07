import { Component, inject, OnInit, signal } from '@angular/core';
import { MemberService } from '../../../core/services/member-service';
import { ActivatedRoute, NavigationEnd, Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { AsyncPipe } from '@angular/common';
import { filter, map, Observable, startWith } from 'rxjs';
import { Member } from '../../../type/Member';
import { AgePipe } from '../../../core/pipes/age-pipe';

@Component({
    selector: 'app-member-detailed',
    imports: [AsyncPipe, RouterLink, RouterLinkActive, RouterOutlet, AgePipe],
    templateUrl: './member-detailed.html',
    styleUrl: './member-detailed.css',
})
export class MemberDetailed implements OnInit {
    private memberService = inject(MemberService);
    private route = inject(ActivatedRoute);
    private router = inject(Router);

    // protected member$?: Observable<Member>
    protected member$!:  Observable<Member | null>;
    protected title$!: Observable<string | undefined>;

    ngOnInit(): void {
        // this.member$ = this.loadMember() as Observable<Member>;
        this.member$ = this.route.data.pipe(                   // 從路由的解析資料中取得成員資料
            map(data => data['member'] as Member | null)       // 將解析出的資料映射為 Member 類型
        )

        this.title$ = this.router.events.pipe(                  // 監聽路由的所有事件
            filter(event => event instanceof NavigationEnd),    // 篩選出 NavigationEnd（路由切換已完成） 事件
            startWith(null), // 讓初始值也能取得
            map(() => this.route.firstChild?.snapshot?.title)   // 取得子路由的 title
        );
    }

    // loadMember() {
    //     const id = this.route.snapshot.paramMap.get('id');
    //     if (!id) return;

    //     return this.memberService.getMemberById(id);
    // }
}
