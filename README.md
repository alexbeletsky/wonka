# WONKA
Candy sweet blog engine with github powered backend for ASP.NET MVC applications.

## Storage
The storage is github repository. The repository contains posts, resources and metadata for blog posts. All data in repository is based on simple conventions.

## Conventions
Folder names and structure

	/ --- Description.yaml
	   |
	   + -- 2011
			|
			+ -- 01
					|
					+ -- 11
					|		+ -- first_post.hmtl
					|		+ -- image.png
					+ -- 17
							+ -- another_post.html
							+ -- code_1.png
							+ -- code_2.png
			+ -- 02
					|
					+ -- 01
							+ -- next_post.html

This folder structure will be mapped to following URL's

	/blog/post/2011/01/11/first-post
	/blog/post/2011/01/17/another-post
	/blog/post/2011/02/01/next-post

### Description.yaml (json)
This file contains blog's meta information including: blog owner, configuration, permissions.

## Caching
TDB.

## Installation
TDB.

## Configuration
TDB.
