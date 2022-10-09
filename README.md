# savings_app

API DOCUMENTATION:

/api/categories -> returns all categories with its subcategories

/api/products -> returns all products
/api/products/{id} -> returns product by id (example: /api/products/1)
/api/products/filter?search={searchText}&filter={filter1}&filter={filter2}&filter={filterX} -> returns product list filtered by search text and all the filters.
Only 1 search parameter can be present in url, but there can be many filters. Filters are the subcategories. (Example : /api/products/filter?search=potato&filter=vegetables&filter=snacks ->
would return all products that contain potato in their name and have either of the filters as their category)

/api/restaurants -> returns all restaurants
/api/restaurants/{id} -> returns restaurant by id (example : /api/restaurants/5)
