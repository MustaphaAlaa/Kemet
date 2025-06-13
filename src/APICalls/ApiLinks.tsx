import ApiDomain from "../app/Models/ApiDomain";



const apiDomain = `${ApiDomain}/api`


const ApiLinks = {
  category: {
    getAll: `${apiDomain}/category/index`,
  },
  color: {
    getAll: `${apiDomain}/color/index`,
    create: `${apiDomain}/a/color`,
    delete: `${apiDomain}/a/color`,
    update: `${apiDomain}/a/color`,
  },
  size: {
    getAll: `${apiDomain}/size/index`,
    create: `${apiDomain}/a/size`,
    delete: `${apiDomain}/a/size`,
    update: `${apiDomain}/a/size`,
  },
  product: {
    create: `${apiDomain}/a/product`,
    get: `${apiDomain}/product`
  },
  productVariant: {
    details: `${apiDomain}/productVariant/details`,
    endpoint: `${apiDomain}/productVariant/details`,
    stock: `${apiDomain}/productVariant/stock`,
  },
  price: {
    get: `${apiDomain}/a/prices/product/range`,
    create: `${apiDomain}/a/prices/price/range`,
    updateNote: `${apiDomain}/a/prices/price/range/note`
  }
};

export default ApiLinks;
