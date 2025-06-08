import ApiDomain from "../app/Models/ApiDomain";

const ApiLinks = {
  category: {
    getAll: `${ApiDomain}/api/category/index`,
  },
  color: {
    getAll: `${ApiDomain}/api/color/index`,
    create: `${ApiDomain}/api/a/color`,
    delete: `${ApiDomain}/api/a/color`,
    update: `${ApiDomain}/api/a/color`,
  },
  size: {
    getAll: `${ApiDomain}/api/size/index`,
    create: `${ApiDomain}/api/a/size`,
    delete: `${ApiDomain}/api/a/size`,
    update: `${ApiDomain}/api/a/size`,
  },
  product: {
    create: `${ApiDomain}/api/product`,
    get: `${ApiDomain}/api/product`
  },
  productVariant: {
    details: `${ApiDomain}/api/productVariant/details`,
    endpoint:  `${ApiDomain}/api/productVariant/details`,
    stock:  `${ApiDomain}/api/productVariant/stock`,
  },
};

export default ApiLinks;
