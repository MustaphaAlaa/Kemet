export default class Dictionary<TKey, TValue> {
  private map: Map<TKey, TValue>;

  constructor() {
    this.map = new Map<TKey, TValue>();
  }

  add(key: TKey, value: TValue): void {
    if (this.map.has(key)) {
      throw new Error(`Duplicate key: ${String(key)}`);
    }
    this.map.set(key, value);
  }

  get(key: TKey): TValue | undefined {  
    return this.map.get(key);
  }

  has(key: TKey): boolean {
    return this.map.has(key);
  }

  delete(key: TKey): boolean {
    return this.map.delete(key);
  }

  entries(): IterableIterator<[TKey, TValue]> {
    return this.map.entries();
  }

  values(): IterableIterator<TValue> {
    return this.map.values();
  }

  keys(): IterableIterator<TKey> {
    return this.map.keys();
  }

  get size(): number {
    return this.map.size;
  }
}
