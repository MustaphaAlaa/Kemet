import ApiDomain from "../app/Models/ApiDomain";



const apiDomain = `${ApiDomain}/api`


const ApiLinks = {
  category: {
    getAll: `${apiDomain}/category/index`,
  },
  color: {
    getAll: `${apiDomain}/color/index`,
    getColor: `${apiDomain}/color`,
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
  },
  productQuantityPrice: {
    quantitiesPrices: `${apiDomain}/productQuantityPrice`,
    createQuantitiesPrices: `${apiDomain}/a/productQuantityPrice`
  },
  deliveryCompany: {
    getAll: `${apiDomain}/a/DeliveryCompany/all`,  
    get: (id: number) => `${apiDomain}a/DeliveryCompany/${id}`,
    create: `${apiDomain}/a/DeliveryCompany`,   
    update: `${apiDomain}/a/DeliveryCompany`,
    delete: `${apiDomain}/a/DeliveryCompany`,
    activeGovernorates: (id: number) => `${apiDomain}/a/DeliveryCompany/${id}/activeGovernorates`,
    updateGovernorateCost: (id:number)=> `${apiDomain}/a/DeliveryCompany/${id}/Governorate`,
  },
  governorateDelivery:{
    admin:{
        getAll: `${apiDomain}/a/governorateDelivery/all`,
        update: `${apiDomain}/a/governorateDelivery`,
    },
    customer:{
        getAll: `${apiDomain}/governorateDelivery/all`
    }
  
  },
};

export default ApiLinks;
