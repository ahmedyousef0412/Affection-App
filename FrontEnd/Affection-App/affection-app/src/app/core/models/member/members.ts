import { Photo } from "./photo"

export interface MembersResponse {
    id: string,
    userName: string
    lookingFor: string
    introduction: string
    interests: string
    country: string
    city: string
    email: string
    mainPhotoUrl: string
    knowAs: string
    gender: string
    age: number
    photos: Photo[]
    createdOn: string
    lastActive: string
}