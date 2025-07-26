export interface ProductWithVariantsCreateDTO {
    Name: string;
    description: string;
    CategoryId: number;
    ColorsIds: number[];
    AllColorsHasSameSizes: boolean | null;
    SizesIds: number[];
    ColorsWithItSizes: {
        [k: string]: number[];
    } | null;
}









