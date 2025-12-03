import { drawerStore } from '../drawer-store';
import { TestBed } from '@angular/core/testing';
import { afterEach, describe, expect, it, vi } from 'vitest';

describe('DrawerStore', () => {
  const getItemSpy = vi.spyOn(Storage.prototype, 'getItem');
  const setItemSpy = vi.spyOn(Storage.prototype, 'setItem');

  it('Should be open by default', () => {
    const tb = TestBed.configureTestingModule({
      providers: [drawerStore],
    });

    getItemSpy.mockReturnValue(null);

    const store = TestBed.inject(drawerStore);

    expect(store.open()).toBe(true);
    store.toggle();
    tb.tick();
    expect(store.open()).toBe(false);
    tb.tick();
  });

  afterEach(() => {
    getItemSpy.mockClear();
    setItemSpy.mockClear();
    localStorage.clear();
  });
});

describe('DrawerStore with localStorage', () => {
  it('Should initialize from localStorage', () => {
    const tb = TestBed.configureTestingModule({
      providers: [drawerStore],
    });
    localStorage.setItem('drawerOpen', 'false');
    const store = TestBed.inject(drawerStore);

    tb.tick();

    expect(store.open()).toBe(false);

    store.toggle();
    tb.tick();

    expect(store.open()).toBe(true);
    expect(localStorage.getItem('drawerOpen')).toBe('true');
  });

  afterEach(() => {
    localStorage.clear();
  });
});
