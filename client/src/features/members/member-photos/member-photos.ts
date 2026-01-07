import { Component, inject } from '@angular/core';
import { MemberService } from '../../../core/services/member-service';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Photo } from '../../../type/Member';
import { AsyncPipe } from '@angular/common';

@Component({
    selector: 'app-member-photos',
    imports: [AsyncPipe],
    templateUrl: './member-photos.html',
    styleUrl: './member-photos.css',
})
export class MemberPhotos {
    private memberService = inject(MemberService);      // 引入 MemberService 以調用 API
    private route = inject(ActivatedRoute);             // 引入 ActivatedRoute，從路由中獲取 memberId

    protected photos$?: Observable<Photo[]>;

    // 在組件初始化時獲取 memberId 並調用 getMemberPhotos 方法
    constructor() {
        const memberId = this.route.parent?.snapshot.paramMap.get('id') || '';
        if (memberId) {
            this.photos$ = this.memberService.getMemberPhotos(memberId);
        }
    }

    get photoMocks(){
        return Array.from({length:12},(_,i)=>`/user.webp`);
    }
}
