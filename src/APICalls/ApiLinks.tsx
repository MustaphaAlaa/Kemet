import ApiDomain from "../app/Models/ApiDomain";

export const ApiLinks = {
  getAllCategories: `${ApiDomain}/api/category/index`,
  getAllColors: `${ApiDomain}/api/color/index`,
  getAllSizes: `${ApiDomain}/api/size/index`,
  productVariantDetails: `${ApiDomain}/api/productVariant/details`,
  product: `${ApiDomain}/api/product`
};
