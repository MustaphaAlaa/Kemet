import React, { useState, useEffect } from 'react';
import { ChevronLeft, ChevronRight, Search } from 'lucide-react';

// Types
interface Product {
  id: number;
  name: string;
  price: number;
  category: string;
  createdDate: string;
}

interface PaginatedResult {
  data: Product[];
  currentPage: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  hasPrevious: boolean;
  hasNext: boolean;
}

// API Service
const productService = {
  async getProducts(
    page: number = 1, 
    pageSize: number = 10, 
    search?: string, 
    category?: string
  ): Promise<PaginatedResult> {
    const params = new URLSearchParams({
      page: page.toString(),
      pageSize: pageSize.toString(),
    });
    
    if (search) params.append('search', search);
    if (category) params.append('category', category);
    
    const response = await fetch(`http://localhost:5000/api/products?${params}`);
    
    if (!response.ok) {
      throw new Error('Failed to fetch products');
    }
    
    return response.json();
  }
};

// Pagination Component
const Pagination = ({ 
  currentPage, 
  totalPages, 
  onPageChange, 
  hasPrevious, 
  hasNext 
}: {
  currentPage: number;
  totalPages: number;
  onPageChange: (page: number) => void;
  hasPrevious: boolean;
  hasNext: boolean;
}) => {
  const getPageNumbers = () => {
    const delta = 2;
    const range = [];
    const rangeWithDots = [];
    
    for (let i = Math.max(2, currentPage - delta); 
         i <= Math.min(totalPages - 1, currentPage + delta); 
         i++) {
      range.push(i);
    }
    
    if (currentPage - delta > 2) { // 5 -2 > 3  is true
      rangeWithDots.push(1, '...');   // [1, ..., range]
    } else {
      rangeWithDots.push(1); // [1] => [range]
    }
    
    rangeWithDots.push(...range); 
    
    if (currentPage + delta < totalPages - 1) { // 5 + 2 < 10 - 1   => 7 < 9 true
      rangeWithDots.push('...', totalPages); // [1,..., range, ..., total pages ]
    } else {
      rangeWithDots.push(totalPages);
    }
    
    return rangeWithDots;
  };

  if (totalPages <= 1) return null;

  return (
    <div className="flex items-center justify-center space-x-2 mt-6">
      <button
        onClick={() => onPageChange(currentPage - 1)}
        disabled={!hasPrevious}
        className="px-3 py-2 text-sm font-medium text-gray-500 bg-white border border-gray-300 rounded-md hover:bg-gray-50 disabled:opacity-50 disabled:cursor-not-allowed"
      >
        <ChevronLeft className="w-4 h-4" />
      </button>
      
      {getPageNumbers().map((page, index) => (
        <button
          key={index}
          onClick={() => typeof page === 'number' && onPageChange(page)}
          disabled={page === '...'}
          className={`px-3 py-2 text-sm font-medium rounded-md ${
            page === currentPage
              ? 'text-white bg-blue-600 border border-blue-600'
              : page === '...'
              ? 'text-gray-500 cursor-default'
              : 'text-gray-500 bg-white border border-gray-300 hover:bg-gray-50'
          }`}
        >
          {page}
        </button>
      ))}
      
      <button
        onClick={() => onPageChange(currentPage + 1)}
        disabled={!hasNext}
        className="px-3 py-2 text-sm font-medium text-gray-500 bg-white border border-gray-300 rounded-md hover:bg-gray-50 disabled:opacity-50 disabled:cursor-not-allowed"
      >
        <ChevronRight className="w-4 h-4" />
      </button>
    </div>
  );
};

// Product Card Component
const ProductCard = ({ product }: { product: Product }) => (
  <div className="bg-white p-6 rounded-lg shadow-md border border-gray-200">
    <h3 className="text-lg font-semibold text-gray-900 mb-2">{product.name}</h3>
    <p className="text-2xl font-bold text-blue-600 mb-2">${product.price.toFixed(2)}</p>
    <p className="text-sm text-gray-600 mb-2">Category: {product.category}</p>
    <p className="text-xs text-gray-500">
      Created: {new Date(product.createdDate).toLocaleDateString()}
    </p>
  </div>
);

// Main Component
const ProductList = () => {
  const [products, setProducts] = useState<PaginatedResult | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [currentPage, setCurrentPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [search, setSearch] = useState('');
  const [category, setCategory] = useState('');

  const fetchProducts = async () => {
    setLoading(true);
    setError(null);
    
    try {
      const result = await productService.getProducts(
        currentPage, 
        pageSize, 
        search || undefined, 
        category || undefined
      );
      setProducts(result);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to fetch products');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchProducts();
  }, [currentPage, pageSize, search, category]);

  const handlePageChange = (page: number) => {
    setCurrentPage(page);
  };

  const handleSearch = () => {
    setCurrentPage(1); // Reset to first page on search
  };

  const handlePageSizeChange = (newPageSize: number) => {
    setPageSize(newPageSize);
    setCurrentPage(1); // Reset to first page
  };

  return (
    <div className="max-w-6xl mx-auto p-6">
      <h1 className="text-3xl font-bold text-gray-900 mb-6">Products</h1>
      
      {/* Filters */}
      <div className="bg-white p-4 rounded-lg shadow-md mb-6">
        <div className="flex flex-col md:flex-row gap-4">
          <div className="flex-1">
            <div className="relative">
              <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-4 h-4" />
              <input
                type="text"
                placeholder="Search products..."
                value={search}
                onChange={(e) => {
                  setSearch(e.target.value);
                  setCurrentPage(1);
                }}
                className="w-full pl-10 pr-4 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 focus:border-transparent"
              />
            </div>
          </div>
          
          <select
            value={category}
            onChange={(e) => {
              setCategory(e.target.value);
              setCurrentPage(1);
            }}
            className="px-4 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 focus:border-transparent"
          >
            <option value="">All Categories</option>
            <option value="Electronics">Electronics</option>
            <option value="Clothing">Clothing</option>
            <option value="Books">Books</option>
          </select>
          
          <select
            value={pageSize}
            onChange={(e) => handlePageSizeChange(Number(e.target.value))}
            className="px-4 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 focus:border-transparent"
          >
            <option value={5}>5 per page</option>
            <option value={10}>10 per page</option>
            <option value={20}>20 per page</option>
            <option value={50}>50 per page</option>
          </select>
        </div>
      </div>

      {/* Loading State */}
      {loading && (
        <div className="flex justify-center items-center py-12">
          <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-blue-600"></div>
        </div>
      )}

      {/* Error State */}
      {error && (
        <div className="bg-red-50 border border-red-200 text-red-700 px-4 py-3 rounded-md mb-6">
          {error}
        </div>
      )}

      {/* Results Info */}
      {products && !loading && (
        <div className="mb-4 text-sm text-gray-600">
          Showing {((products.currentPage - 1) * products.pageSize) + 1} to{' '}
          {Math.min(products.currentPage * products.pageSize, products.totalCount)} of{' '}
          {products.totalCount} results
        </div>
      )}

      {/* Products Grid */}
      {products && products.data.length > 0 ? (
        <>
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 mb-6">
            {products.data.map((product) => (
              <ProductCard key={product.id} product={product} />
            ))}
          </div>
          
          <Pagination
            currentPage={products.currentPage}
            totalPages={products.totalPages}
            onPageChange={handlePageChange}
            hasPrevious={products.hasPrevious}
            hasNext={products.hasNext}
          />
        </>
      ) : (
        !loading && (
          <div className="text-center py-12">
            <p className="text-gray-500">No products found.</p>
          </div>
        )
      )}
    </div>
  );
};

export default ProductList;