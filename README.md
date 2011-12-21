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

## Installation
It should be installed via NuGet package. The package would contain all required Views/Models/Controller to be able to run blog engine. All updates are recieved via NuGet as well.

It is possible to integrate wonka into any ASP.NET MVC or ASP.NET WebForms application. Separate 'Area' call 'blog' is installed, so blog accessible by http://yoursite.com/blog.

## Configuration
At first visit of /blog URL the configuration wizard appears. It includes just basic configuration parameters as: github account name, repository, themes. 

## Caching
To prevent multiple github API requests Wonka provides simple caching capabilies. Lightweight embedded database will be used to cache prepared posts. Currently I haven't decieded which to choose: SQL Lite or SQL CE.

## Themes
Several minimalistic themes are supported. 
